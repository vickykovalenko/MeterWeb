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
    public class ReadingsController : Controller
    {
        private readonly DBLibraryContext _context;

        public ReadingsController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: Readings
        public async Task<IActionResult> Index()
        {
            var dBLibraryContext = _context.Readings.Include(r => r.ReadingMeter).Include(r => r.ReadingPayment);
            return View(await dBLibraryContext.ToListAsync());
        }

        // GET: Readings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reading = await _context.Readings
                .Include(r => r.ReadingMeter)
                .Include(r => r.ReadingPayment)
                .FirstOrDefaultAsync(m => m.ReadingId == id);
            if (reading == null)
            {
                return NotFound();
            }

            return View(reading);
        }

        // GET: Readings/Create
        public IActionResult Create()
        {
            ViewData["ReadingMeterId"] = new SelectList(_context.Meters, "MeterId", "MeterId");
            ViewData["ReadingPaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId");
            return View();
        }

        // POST: Readings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReadingId,ReadingDataOfCurrentReading,ReadingMeterId,ReadingPaymentId")] Reading reading)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reading);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReadingMeterId"] = new SelectList(_context.Meters, "MeterId", "MeterId", reading.ReadingMeterId);
            ViewData["ReadingPaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId", reading.ReadingPaymentId);
            return View(reading);
        }

        // GET: Readings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reading = await _context.Readings.FindAsync(id);
            if (reading == null)
            {
                return NotFound();
            }
            ViewData["ReadingMeterId"] = new SelectList(_context.Meters, "MeterId", "MeterId", reading.ReadingMeterId);
            ViewData["ReadingPaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId", reading.ReadingPaymentId);
            return View(reading);
        }

        // POST: Readings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReadingId,ReadingDataOfCurrentReading,ReadingMeterId,ReadingPaymentId")] Reading reading)
        {
            if (id != reading.ReadingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reading);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReadingExists(reading.ReadingId))
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
            ViewData["ReadingMeterId"] = new SelectList(_context.Meters, "MeterId", "MeterId", reading.ReadingMeterId);
            ViewData["ReadingPaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId", reading.ReadingPaymentId);
            return View(reading);
        }

        // GET: Readings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reading = await _context.Readings
                .Include(r => r.ReadingMeter)
                .Include(r => r.ReadingPayment)
                .FirstOrDefaultAsync(m => m.ReadingId == id);
            if (reading == null)
            {
                return NotFound();
            }

            return View(reading);
        }

        // POST: Readings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reading = await _context.Readings.FindAsync(id);
            _context.Readings.Remove(reading);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReadingExists(int id)
        {
            return _context.Readings.Any(e => e.ReadingId == id);
        }
    }
}
