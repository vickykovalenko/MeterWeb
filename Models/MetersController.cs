using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeterWeb;

namespace MeterWeb.Models
{
    public class MetersController : Controller
    {
        private readonly DBLibraryContext _context;

        public MetersController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: Meters
        public async Task<IActionResult> Index()
        {
            var dBLibraryContext = _context.Meters.Include(m => m.MeterFlat).Include(m => m.MeterType);
            return View(await dBLibraryContext.ToListAsync());
        }

        // GET: Meters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meter = await _context.Meters
                .Include(m => m.MeterFlat)
                .Include(m => m.MeterType)
                .FirstOrDefaultAsync(m => m.MeterId == id);
            if (meter == null)
            {
                return NotFound();
            }

            return View(meter);
        }

        // GET: Meters/Create
        public IActionResult Create()
        {
            ViewData["MeterFlatId"] = new SelectList(_context.Flats, "FlatId", "FlatAddress");
            ViewData["MeterTypeId"] = new SelectList(_context.MeterTypes, "MeterTypeId", "MeterTypeName");
            return View();
        }

        // POST: Meters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeterId,MeterNumbers,MeterTypeId,MeterFlatId,MeterDataLastReplacement")] Meter meter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MeterFlatId"] = new SelectList(_context.Flats, "FlatId", "FlatAddress", meter.MeterFlatId);
            ViewData["MeterTypeId"] = new SelectList(_context.MeterTypes, "MeterTypeId", "MeterTypeName", meter.MeterTypeId);
            return View(meter);
        }

        // GET: Meters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meter = await _context.Meters.FindAsync(id);
            if (meter == null)
            {
                return NotFound();
            }
            ViewData["MeterFlatId"] = new SelectList(_context.Flats, "FlatId", "FlatAddress", meter.MeterFlatId);
            ViewData["MeterTypeId"] = new SelectList(_context.MeterTypes, "MeterTypeId", "MeterTypeName", meter.MeterTypeId);
            return View(meter);
        }

        // POST: Meters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeterId,MeterNumbers,MeterTypeId,MeterFlatId,MeterDataLastReplacement")] Meter meter)
        {
            if (id != meter.MeterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeterExists(meter.MeterId))
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
            ViewData["MeterFlatId"] = new SelectList(_context.Flats, "FlatId", "FlatAddress", meter.MeterFlatId);
            ViewData["MeterTypeId"] = new SelectList(_context.MeterTypes, "MeterTypeId", "MeterTypeName", meter.MeterTypeId);
            return View(meter);
        }

        // GET: Meters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meter = await _context.Meters
                .Include(m => m.MeterFlat)
                .Include(m => m.MeterType)
                .FirstOrDefaultAsync(m => m.MeterId == id);
            if (meter == null)
            {
                return NotFound();
            }

            return View(meter);
        }

        // POST: Meters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meter = await _context.Meters.FindAsync(id);
            _context.Meters.Remove(meter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeterExists(int id)
        {
            return _context.Meters.Any(e => e.MeterId == id);
        }
    }
}
