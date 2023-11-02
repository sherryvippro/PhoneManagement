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
    public class TChiTietHdbsController : Controller
    {
        private readonly QLBanDTContext _context;

        public TChiTietHdbsController(QLBanDTContext context)
        {
            _context = context;
        }

        // GET: TChiTietHdbs
        public async Task<IActionResult> Index(string id)
        {
            var qLBanDTContext = _context.TChiTietHdbs.Where(t => t.SoHdb == id)
                .Include(t => t.MaSpNavigation).Include(t => t.SoHdbNavigation)
                .Include(t => t.SoHdbNavigation.MaKhNavigation);
            return View(await qLBanDTContext.ToListAsync());
        }

        // GET: TChiTietHdbs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TChiTietHdbs == null)
            {
                return NotFound();
            }

            var tChiTietHdb = await _context.TChiTietHdbs
                .Include(t => t.MaSpNavigation)
                .Include(t => t.SoHdbNavigation)
                .FirstOrDefaultAsync(m => m.SoHdb == id);
            if (tChiTietHdb == null)
            {
                return NotFound();
            }

            return View(tChiTietHdb);
        }

        // GET: TChiTietHdbs/Create
        public IActionResult Create()
        {
            ViewData["MaSp"] = new SelectList(_context.TSp, "MaSp", "MaSp");
            ViewData["SoHdb"] = new SelectList(_context.THoaDonBans, "SoHdb", "SoHdb");
            return View();
        }

        // POST: TChiTietHdbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoHdb,MaSp,Slban,KhuyenMai")] TChiTietHdb tChiTietHdb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tChiTietHdb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaSp"] = new SelectList(_context.TSp, "MaSp", "MaSp", tChiTietHdb.MaSp);
            ViewData["SoHdb"] = new SelectList(_context.THoaDonBans, "SoHdb", "SoHdb", tChiTietHdb.SoHdb);
            return View(tChiTietHdb);
        }

        // GET: TChiTietHdbs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TChiTietHdbs == null)
            {
                return NotFound();
            }

            var tChiTietHdb = await _context.TChiTietHdbs.FindAsync(id);
            if (tChiTietHdb == null)
            {
                return NotFound();
            }
            ViewData["MaSp"] = new SelectList(_context.TSp, "MaSp", "MaSp", tChiTietHdb.MaSp);
            ViewData["SoHdb"] = new SelectList(_context.THoaDonBans, "SoHdb", "SoHdb", tChiTietHdb.SoHdb);
            return View(tChiTietHdb);
        }

        // POST: TChiTietHdbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SoHdb,MaSp,Slban,KhuyenMai")] TChiTietHdb tChiTietHdb)
        {
            if (id != tChiTietHdb.SoHdb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tChiTietHdb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TChiTietHdbExists(tChiTietHdb.SoHdb))
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
            ViewData["MaSp"] = new SelectList(_context.TSp, "MaSp", "MaSp", tChiTietHdb.MaSp);
            ViewData["SoHdb"] = new SelectList(_context.THoaDonBans, "SoHdb", "SoHdb", tChiTietHdb.SoHdb);
            return View(tChiTietHdb);
        }

        // GET: TChiTietHdbs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TChiTietHdbs == null)
            {
                return NotFound();
            }

            var tChiTietHdb = await _context.TChiTietHdbs
                .Include(t => t.MaSpNavigation)
                .Include(t => t.SoHdbNavigation)
                .FirstOrDefaultAsync(m => m.SoHdb == id);
            if (tChiTietHdb == null)
            {
                return NotFound();
            }

            return View(tChiTietHdb);
        }

        // POST: TChiTietHdbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TChiTietHdbs == null)
            {
                return Problem("Entity set 'QLBanDTContext.TChiTietHdbs'  is null.");
            }
            var tChiTietHdb = await _context.TChiTietHdbs.FindAsync(id);
            if (tChiTietHdb != null)
            {
                _context.TChiTietHdbs.Remove(tChiTietHdb);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TChiTietHdbExists(string id)
        {
          return (_context.TChiTietHdbs?.Any(e => e.SoHdb == id)).GetValueOrDefault();
        }
    }
}
