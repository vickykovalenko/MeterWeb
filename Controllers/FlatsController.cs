using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MeterWeb;
using System.IO;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using MeterWeb.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;



namespace MeterWeb.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class FlatsController : Controller
    {
        private readonly DBLibraryContext _context;
        private readonly ClaimsPrincipal _user;
        private readonly UserManager<User> _userManager;
        public FlatsController(DBLibraryContext context, IHttpContextAccessor accessor, UserManager<User> userManager)
        {
            _context = context;
            _user = accessor.HttpContext.User;
            _userManager = userManager;

        }

        // GET: Flats
        public async Task<IActionResult> Index()
        {
            return View(await _context.Flats.ToListAsync());
        }

        // GET: Flats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flat = await _context.Flats
                .FirstOrDefaultAsync(m => m.FlatId == id);
            if (flat == null)
            {
                return NotFound();
            }

            //return View(flat);
            return RedirectToAction("Index", "Meters", new { id = flat.FlatId, address = flat.FlatAddress});
        }
        //[Authorize(Roles = "admin, user")]
        // GET: Flats/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "admin, user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlatId,FlatAddress")] Flat flat)
        {
            if (ModelState.IsValid)
            {
                flat.UserId = _userManager.GetUserId(HttpContext.User);
                _context.Add(flat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(flat);
        }

        // GET: Flats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flat = await _context.Flats.FindAsync(id);
            if (flat == null)
            {
                return NotFound();
            }
            return View(flat);
        }

        // POST: Flats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlatId,FlatAddress")] Flat flat)
        {
            if (id != flat.FlatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlatExists(flat.FlatId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flat);
        }

        // GET: Flats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flat = await _context.Flats
                .FirstOrDefaultAsync(m => m.FlatId == id);
            if (flat == null)
            {
                return NotFound();
            }

            return View(flat);
        }

        // POST: Flats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flat = await _context.Flats.FindAsync(id);
            var meterIds = _context.Meters.Where(m => m.MeterFlatId == id);



            foreach (var item in meterIds)
            {

                var readingIds = _context.Readings.Where(r => r.ReadingMeterId == item.MeterId);
                foreach (var item2 in readingIds)
                {
                    _context.Readings.Remove(item2);
                }

                _context.Meters.Remove(item);
            }
            _context.Flats.Remove(flat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlatExists(int id)
        {
            return _context.Flats.Any(e => e.FlatId == id);
        }
    }
}
