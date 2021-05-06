using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Http;
using GemBox.Document;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MeterWeb.Controllers
{

    public class MetersController : Controller
    {
        private readonly DBLibraryContext _context;

        public MetersController(DBLibraryContext context)
        {
            _context = context;
        }


        // GET: Meters
        public async Task<IActionResult> Index(int? id, string? address)
        {
            if (id == null) return RedirectToAction("Index", "Flats");
            //знаходження квартир за адресою
            ViewBag.FlatId = id;
            ViewBag.FlatAddress = address;
            var meterByFlat = _context.Meters.Where(m => m.MeterFlatId == id).Include(m => m.MeterFlat).Include(m => m.MeterType);
            //var dBLibraryContext = _context.Meters.Include(m => m.MeterFlat).Include(m => m.MeterType);
            //return View(await dBLibraryContext.ToListAsync());
            return View(await meterByFlat.ToListAsync());
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

            //return View(meter);
            return RedirectToAction("Index", "Readings", new { id = meter.MeterId, numbers = meter.MeterNumbers });
        }

        // GET: Meters/Create
        public IActionResult Create(int flatId)
        {
            //ViewData["MeterFlatId"] = new SelectList(_context.Flats, "FlatId", "FlatAddress");
            ViewData["MeterTypeId"] = new SelectList(_context.MeterTypes, "MeterTypeId", "MeterTypeName");
            ViewBag.MeterFlatId = flatId;

            ViewBag.FlatAddress = _context.Flats.Where(f => f.FlatId == flatId).FirstOrDefault().FlatAddress;
            return View();
        }

        // POST: Meters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int flatId, [Bind("MeterId,MeterNumbers,MeterTypeId, MeterFlatId,MeterDataLastReplacement")] Meter meter)
        {
            meter.MeterFlatId = flatId;
            if (ModelState.IsValid)
            {
                _context.Add(meter);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Meters", new { id = flatId, address = _context.Flats.Where(f => f.FlatId == flatId).FirstOrDefault().FlatAddress });
            }
            //ViewData["MeterFlatId"] = new SelectList(_context.Flats, "FlatId", "FlatAddress", meter.MeterFlatId);
            //ViewData["MeterTypeId"] = new SelectList(_context.MeterTypes, "MeterTypeId", "MeterTypeName", meter.MeterTypeId);

            return View(meter);

            // return RedirectToAction("Index", "Meters", new { MeterId = flatId, name = _context.Flats.Where(f => f.FlatId == flatId).FirstOrDefault().FlatAddress });
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public IActionResult MeterList() => View(_context.Meters.ToList());
        // POST: Meters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int flatId)
        {
            var meter = await _context.Meters.FindAsync(id);
            var readingIds = _context.Readings.Where(r => r.ReadingMeterId == id);
            foreach (var item in readingIds)
            {
                _context.Readings.Remove(item);
            }
            _context.Meters.Remove(meter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            
        }

        private bool MeterExists(int id)
        {
            return _context.Meters.Any(e => e.MeterId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel, int flatId)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            //перегляд усіх листів (в даному випадку видів лічильників)
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                                //worksheet.Name - назва типу лічильника. Пробуємо знайти в БД, якщо відсутній, то створюємо новий
                                MeterType metertype;
                                var m = (from met in _context.MeterTypes
                                         where met.MeterTypeName.Contains(worksheet.Name)
                                         select met).ToList();
                                if (m.Count > 0)
                                {
                                    metertype = m[0];
                                }
                                else
                                {
                                    metertype = new MeterType();
                                    metertype.MeterTypeName = worksheet.Name;

                                    //додати в контекст
                                    _context.MeterTypes.Add(metertype);

                                }
                                //перегляд усіх рядків                    
                                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                                {
                                    try
                                    {
                                        Meter meter = new Meter();
                                        //meter.MeterFlatId = Convert.ToInt32(row.Cell(1).Value);
                                        meter.MeterFlatId = flatId;
                                        meter.MeterNumbers = Convert.ToInt32(row.Cell(1).Value);
                                        meter.MeterDataLastReplacement = Convert.ToDateTime(row.Cell(2).Value);
                                        meter.MeterTypeId = metertype.MeterTypeId;
                                        

                                        _context.Meters.Add(meter);
                                    }
                                    catch (Exception e)
                                    {

                                        return View();
                                    }
                                }

                            }
                        }
                    }
                }


        await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var metertypes = _context.MeterTypes.Include("Meters").ToList();
                //тут, для прикладу ми пишемо усі книжки з БД, в своїх проектах ТАК НЕ РОБИТИ (писати лише вибрані)
                foreach (var m in metertypes)
                {
                    var worksheet = workbook.Worksheets.Add(m.MeterTypeName);
                    worksheet.Cell("A1").Value = "MeterFlatId";

                    worksheet.Cell("B1").Value = "MeterNumbers";
                    worksheet.Cell("C1").Value = "DataLastReplacement";
                   

                    worksheet.Row(1).Style.Font.Bold = true;
                    var meters = m.Meters.ToList();

                    //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                    for (int i = 0; i < meters.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = meters[i].MeterFlatId;
                        worksheet.Cell(i + 2, 2).Value = meters[i].MeterNumbers;
                        worksheet.Cell(i + 2, 3).Value = meters[i].MeterDataLastReplacement;





                    }

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"library_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }
    }
}

