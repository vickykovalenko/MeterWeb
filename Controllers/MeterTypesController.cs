using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeterWeb;

namespace MeterWeb.Controllers
{
    public class MeterTypesController : Controller
    {
        private readonly DBLibraryContext _context;

        public MeterTypesController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: MeterTypes
        public async Task<IActionResult> Index()
        {
            var dBLibraryContext = _context.MeterTypes.Include(m => m.MeterService);
            return View(await dBLibraryContext.ToListAsync());
        }

        // GET: MeterTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meterType = await _context.MeterTypes
                .Include(m => m.MeterService)
                .FirstOrDefaultAsync(m => m.MeterTypeId == id);
            if (meterType == null)
            {
                return NotFound();
            }

            return View(meterType);
        }

        // GET: MeterTypes/Create
        public IActionResult Create()
        {
            ViewData["MeterServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName");
            return View();
        }

        // POST: MeterTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeterTypeId,MeterTypeName,MeterServiceId")] MeterType meterType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meterType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MeterServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName", meterType.MeterServiceId);
            return View(meterType);
        }

        // GET: MeterTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meterType = await _context.MeterTypes.FindAsync(id);
            if (meterType == null)
            {
                return NotFound();
            }
            ViewData["MeterServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName", meterType.MeterServiceId);
            return View(meterType);
        }

        // POST: MeterTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeterTypeId,MeterTypeName,MeterServiceId")] MeterType meterType)
        {
            if (id != meterType.MeterTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meterType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeterTypeExists(meterType.MeterTypeId))
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
            ViewData["MeterServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName", meterType.MeterServiceId);
            return View(meterType);
        }

        // GET: MeterTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meterType = await _context.MeterTypes
                .Include(m => m.MeterService)
                .FirstOrDefaultAsync(m => m.MeterTypeId == id);
            if (meterType == null)
            {
                return NotFound();
            }

            return View(meterType);
        }

        // POST: MeterTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meterType = await _context.MeterTypes.FindAsync(id);
            _context.MeterTypes.Remove(meterType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeterTypeExists(int id)
        {
            return _context.MeterTypes.Any(e => e.MeterTypeId == id);
        }
    }
}
