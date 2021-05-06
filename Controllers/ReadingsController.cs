using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeterWeb;
using MoreLinq;
using MoreLinq.Extensions;

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
        public async Task<IActionResult> Index(int? id, int numbers)
        {
            if (id == null) return RedirectToAction("Index", "Flats");
            //покази за лічильником
            ViewBag.MeterId = id;
            ViewBag.MeterNumbers = numbers;
           

            var readingsByMeter = _context.Readings.Where(r => r.ReadingMeterId == id).Include(r => r.ReadingPayment);
            

            //var dBLibraryContext = _context.Readings.Include(r => r.ReadingMeter).Include(r => r.ReadingDataOfCurrentReading).Include(r => r.ReadingPayment);
            //return View(await dBLibraryContext.ToListAsync());
            return View(await readingsByMeter.ToListAsync());
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

            //return View(reading);
            return RedirectToAction("Index", "Payments", new { id = reading.ReadingId, number = reading.ReadingNumber });
        }

        // GET: Readings/Create
        public IActionResult Create(int meterId)
        {
           // ViewData["ReadingMeterId"] = new SelectList(_context.Meters, "MeterId", "MeterId");
           // ViewData["ReadingPaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId");
            ViewBag.MeterId = meterId;
            ViewBag.ReadingPaymentId = 1;
            ViewBag.MeterDataLastReplaceMent = _context.Meters.Where(m => m.MeterId == meterId).FirstOrDefault().MeterDataLastReplacement;

            return View();
        }

        // POST: Readings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int meterId, [Bind("PaymentTariffIdReadingId,ReadingDataOfCurrentReading,ReadingMeterId,ReadingPaymentId, ReadingNumber")] Reading reading)
        {
            //reading.ReadingMeterId = meterId;
            if (ModelState.IsValid)
            {
                reading.ReadingPaymentId = 2;
                _context.Add(reading);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Readings", new { id = meterId, numbers = _context.Meters.Where(m => m.MeterId == meterId).FirstOrDefault().MeterNumbers });
            }
            //ViewData["ReadingMeterId"] = new SelectList(_context.Meters, "MeterId", "MeterId", reading.ReadingMeterId);
            //ViewData["ReadingPaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId", reading.ReadingPaymentId);
            //return RedirectToAction("Index", "Readings", new { ReadingId = meterId, numbers = _context.Meters.Where(m => m.MeterId == meterId).FirstOrDefault().MeterNumbers });
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
        public async Task<IActionResult> Edit(int id, [Bind("ReadingId,ReadingDataOfCurrentReading,ReadingMeterId,ReadingPaymentId, ReadingNumber")] Reading reading)
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
