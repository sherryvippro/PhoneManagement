using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin.Models;
using Admin.Services;

namespace Admin.Controllers
{
    public class THoaDonNhapsController : Controller
    {
        private readonly QLBanDTContext _context;
        private readonly InvoiceServices _invoiceServices;

        public THoaDonNhapsController(QLBanDTContext context, InvoiceServices invoiceServices)
        {
            _context = context;
            _invoiceServices = invoiceServices;
        }

        // GET: THoaDonNhaps
        public async Task<IActionResult> Index()
        {
            var qLBanDTContext = _context.THoaDonNhaps.Include(t => t.MaNccNavigation);
            return View(await qLBanDTContext.ToListAsync());
        }

        // GET: THoaDonNhaps/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.THoaDonNhaps == null)
            {
                return NotFound();
            }

            var tHoaDonNhap = await _context.THoaDonNhaps
                .Include(t => t.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.SoHdn == id);
            if (tHoaDonNhap == null)
            {
                return NotFound();
            }

            return View(tHoaDonNhap);
        }

        // GET: THoaDonNhaps/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.SoHDN = await _invoiceServices.GenerateSHDNAsync();
            ViewData["MaNcc"] = new SelectList(_context.TNhaCungCaps, "MaNcc", "TenNcc");
            return View();
        }

        // POST: THoaDonNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoHdn,NgayNhap,MaNcc,TongHdn")] THoaDonNhap tHoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tHoaDonNhap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SoHDN = await _invoiceServices.GenerateSHDNAsync();
            ViewData["MaNcc"] = new SelectList(_context.TNhaCungCaps, "MaNcc", "TenNcc", tHoaDonNhap.MaNcc);
            return View(tHoaDonNhap);
        }

        // GET: THoaDonNhaps/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.THoaDonNhaps == null)
            {
                return NotFound();
            }

            var tHoaDonNhap = await _context.THoaDonNhaps.FindAsync(id);
            if (tHoaDonNhap == null)
            {
                return NotFound();
            }
            ViewData["MaNcc"] = new SelectList(_context.TNhaCungCaps, "MaNcc", "TenNcc", tHoaDonNhap.MaNcc);
            return View(tHoaDonNhap);
        }

        // POST: THoaDonNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SoHdn,NgayNhap,MaNcc,TongHdn")] THoaDonNhap tHoaDonNhap)
        {
            if (id != tHoaDonNhap.SoHdn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tHoaDonNhap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!THoaDonNhapExists(tHoaDonNhap.SoHdn))
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
            ViewData["MaNcc"] = new SelectList(_context.TNhaCungCaps, "MaNcc", "TenNcc", tHoaDonNhap.MaNcc);
            return View(tHoaDonNhap);
        }

        // GET: THoaDonNhaps/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.THoaDonNhaps == null)
            {
                return NotFound();
            }

            var tHoaDonNhap = await _context.THoaDonNhaps
                .Include(t => t.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.SoHdn == id);
            if (tHoaDonNhap == null)
            {
                return NotFound();
            }

            return View(tHoaDonNhap);
        }

        // POST: THoaDonNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.THoaDonNhaps == null)
            {
                return Problem("Entity set 'QLBanDTContext.THoaDonNhaps'  is null.");
            }
            var tHoaDonNhap = await _context.THoaDonNhaps.FindAsync(id);
            if (tHoaDonNhap != null)
            {
                _context.THoaDonNhaps.Remove(tHoaDonNhap);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool THoaDonNhapExists(string id)
        {
          return (_context.THoaDonNhaps?.Any(e => e.SoHdn == id)).GetValueOrDefault();
        }

        
    }
}
