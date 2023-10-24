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
    public class TAdminsController : Controller
    {
        private readonly QLBanDTContext _context;

        public TAdminsController(QLBanDTContext context)
        {
            _context = context;
        }

        // GET: TAdmins
        [Route("Admins/List/Chunhunancut")]
        public async Task<IActionResult> Index()
        {
              return _context.TAdmins != null ? 
                          View(await _context.TAdmins.ToListAsync()) :
                          Problem("Entity set 'QLBanDTContext.TAdmins'  is null.");
        }

        // GET: TAdmins/Details/5
        [Route("TAdmins/Detail")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TAdmins == null)
            {
                return NotFound();
            }

            var tAdmin = await _context.TAdmins
                .FirstOrDefaultAsync(m => m.MaAdmin == id);
            if (tAdmin == null)
            {
                return NotFound();
            }

            return View(tAdmin);
        }

        // GET: TAdmins/Create
        [Route("TAdmins/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaAdmin,UserName,PassWord,Role")] TAdmin tAdmin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tAdmin);
        }

        // GET: TAdmins/Edit/5
        [Route("TAdmins/Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TAdmins == null)
            {
                return NotFound();
            }

            var tAdmin = await _context.TAdmins.FindAsync(id);
            if (tAdmin == null)
            {
                return NotFound();
            }
            return View(tAdmin);
        }

        // POST: TAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaAdmin,UserName,PassWord,Role")] TAdmin tAdmin)
        {
            if (id != tAdmin.MaAdmin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TAdminExists(tAdmin.MaAdmin))
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
            return View(tAdmin);
        }

        // GET: TAdmins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TAdmins == null)
            {
                return NotFound();
            }

            var tAdmin = await _context.TAdmins
                .FirstOrDefaultAsync(m => m.MaAdmin == id);
            if (tAdmin == null)
            {
                return NotFound();
            }

            return View(tAdmin);
        }

        // POST: TAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TAdmins == null)
            {
                return Problem("Entity set 'QLBanDTContext.TAdmins'  is null.");
            }
            var tAdmin = await _context.TAdmins.FindAsync(id);
            if (tAdmin != null)
            {
                _context.TAdmins.Remove(tAdmin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TAdminExists(string id)
        {
          return (_context.TAdmins?.Any(e => e.MaAdmin == id)).GetValueOrDefault();
        }
    }
}
