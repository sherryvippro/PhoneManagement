using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin.Models;
using Admin.Models.ViewModels;
using System.Drawing.Printing;
using Admin.Services;

namespace Admin.Controllers
{
    public class THoaDonNhapsController : Controller
    {

        private readonly QLBanDTContext _context;
        private readonly ProductServices _productServices;
        public int pageSize = 10;


        public THoaDonNhapsController(QLBanDTContext context)
        {
            _context = context;
        }

        // GET: THoaDonNhaps
        public async Task<IActionResult> Index(int invoiceInPage = 1)
        {
            return View(
                new InvoiceInListViewModel
                {
                    InvoiceIn = _context.TChiTietHdns.Include(t => t.SoHdnNavigation).Include(t => t.MaSpNavigation)
                    .Include(t => t.MaSpNavigation)
                    .Skip((invoiceInPage - 1) * pageSize).Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        itemsPerPage = pageSize,
                        currentPage = invoiceInPage,
                        totalItem = _context.TChiTietHdbs.Count()
                    }
                }
            );
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
        public IActionResult Create()
        {
            var tHoaDonNhap = new THoaDonNhap();
            var tChiTietHDN = new TChiTietHdn();
            tChiTietHDN.list = new List<TSp>();
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
            ViewData["MaNcc"] = new SelectList(_context.TNhaCungCaps, "MaNcc", "MaNcc", tHoaDonNhap.MaNcc);
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
            ViewData["MaNcc"] = new SelectList(_context.TNhaCungCaps, "MaNcc", "MaNcc", tHoaDonNhap.MaNcc);
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
