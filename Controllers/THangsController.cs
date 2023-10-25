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
    public class THangsController : Controller
    {
        private readonly QLBanDTContext _context;

        public THangsController(QLBanDTContext context)
        {
            _context = context;
        }

        // GET: THangs
        public async Task<IActionResult> Index()
        {
              return _context.TKhachHangs != null ? 
                          View(await _context.TKhachHangs.ToListAsync()) :
                          Problem("Entity set 'QLBanDTContext.TKhachHangs'  is null.");
        }

        // GET: THangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TKhachHangs == null)
            {
                return NotFound();
            }

            var tKhachHang = await _context.TKhachHangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (tKhachHang == null)
            {
                return NotFound();
            }

            return View(tKhachHang);
        }

        // GET: THangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: THangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKh,TenKh,GioiTinh,DiaChi,DienThoai,Email")] TKhachHang tKhachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tKhachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tKhachHang);
        }

        // GET: THangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TKhachHangs == null)
            {
                return NotFound();
            }

            var tKhachHang = await _context.TKhachHangs.FindAsync(id);
            if (tKhachHang == null)
            {
                return NotFound();
            }
            return View(tKhachHang);
        }

        // POST: THangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKh,TenKh,GioiTinh,DiaChi,DienThoai,Email")] TKhachHang tKhachHang)
        {
            if (id != tKhachHang.MaKh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tKhachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TKhachHangExists(tKhachHang.MaKh))
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
            return View(tKhachHang);
        }

        // GET: THangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TKhachHangs == null)
            {
                return NotFound();
            }

            var tKhachHang = await _context.TKhachHangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (tKhachHang == null)
            {
                return NotFound();
            }

            return View(tKhachHang);
        }

        // POST: THangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TKhachHangs == null)
            {
                return Problem("Entity set 'QLBanDTContext.TKhachHangs'  is null.");
            }
            var tKhachHang = await _context.TKhachHangs.FindAsync(id);
            if (tKhachHang != null)
            {
                _context.TKhachHangs.Remove(tKhachHang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TKhachHangExists(string id)
        {
          return (_context.TKhachHangs?.Any(e => e.MaKh == id)).GetValueOrDefault();
        }
    }
}
