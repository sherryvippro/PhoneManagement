using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin.Models;

namespace Admin.Controllers
{
    public class TChiTietHdnsController : Controller
    {
        private readonly QLBanDTContext _context;

        public TChiTietHdnsController(QLBanDTContext context)
        {
            _context = context;
        }

        // GET: TChiTietHdns
        public async Task<IActionResult> Index()
        {
            var qLBanDTContext = _context.TChiTietHdns.Include(t => t.MaSpNavigation).Include(t => t.SoHdnNavigation);
            return View(await qLBanDTContext.ToListAsync());
        }

        // GET: TChiTietHdns/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TChiTietHdns == null)
            {
                return NotFound();
            }

            var tChiTietHdn = await _context.TChiTietHdns
                .Include(t => t.MaSpNavigation)
                .Include(t => t.SoHdnNavigation)
                .FirstOrDefaultAsync(m => m.SoHdn == id);
            if (tChiTietHdn == null)
            {
                return NotFound();
            }

            return View(tChiTietHdn);
        }

        // GET: TChiTietHdns/Create
        public IActionResult Create()
        {
            ViewData["MaSp"] = new SelectList(_context.TSp, "MaSp", "MaSp");
            ViewData["SoHdn"] = new SelectList(_context.THoaDonNhaps, "SoHdn", "SoHdn");
            return View();
        }

        // POST: TChiTietHdns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoHdn,MaSp,Slnhap,KhuyenMai")] TChiTietHdn tChiTietHdn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tChiTietHdn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaSp"] = new SelectList(_context.TSp, "MaSp", "MaSp", tChiTietHdn.MaSp);
            ViewData["SoHdn"] = new SelectList(_context.THoaDonNhaps, "SoHdn", "SoHdn", tChiTietHdn.SoHdn);
            return View(tChiTietHdn);
        }

        // GET: TChiTietHdns/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TChiTietHdns == null)
            {
                return NotFound();
            }

            var tChiTietHdn = await _context.TChiTietHdns.FindAsync(id);
            if (tChiTietHdn == null)
            {
                return NotFound();
            }
            ViewData["MaSp"] = new SelectList(_context.TSp, "MaSp", "MaSp", tChiTietHdn.MaSp);
            ViewData["SoHdn"] = new SelectList(_context.THoaDonNhaps, "SoHdn", "SoHdn", tChiTietHdn.SoHdn);
            return View(tChiTietHdn);
        }

        // POST: TChiTietHdns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SoHdn,MaSp,Slnhap,KhuyenMai")] TChiTietHdn tChiTietHdn)
        {
            if (id != tChiTietHdn.SoHdn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tChiTietHdn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TChiTietHdnExists(tChiTietHdn.SoHdn))
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
            ViewData["MaSp"] = new SelectList(_context.TSp, "MaSp", "MaSp", tChiTietHdn.MaSp);
            ViewData["SoHdn"] = new SelectList(_context.THoaDonNhaps, "SoHdn", "SoHdn", tChiTietHdn.SoHdn);
            return View(tChiTietHdn);
        }

        // GET: TChiTietHdns/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TChiTietHdns == null)
            {
                return NotFound();
            }

            var tChiTietHdn = await _context.TChiTietHdns
                .Include(t => t.MaSpNavigation)
                .Include(t => t.SoHdnNavigation)
                .FirstOrDefaultAsync(m => m.SoHdn == id);
            if (tChiTietHdn == null)
            {
                return NotFound();
            }

            return View(tChiTietHdn);
        }

        // POST: TChiTietHdns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TChiTietHdns == null)
            {
                return Problem("Entity set 'QLBanDTContext.TChiTietHdns'  is null.");
            }
            var tChiTietHdn = await _context.TChiTietHdns.FindAsync(id);
            if (tChiTietHdn != null)
            {
                _context.TChiTietHdns.Remove(tChiTietHdn);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TChiTietHdnExists(string id)
        {
          return (_context.TChiTietHdns?.Any(e => e.SoHdn == id)).GetValueOrDefault();
        }
    }
}
