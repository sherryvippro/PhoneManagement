﻿using System;
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
    public class TSpsController : Controller
    {
        private readonly QLBanDTContext _context;
        private readonly ProductServices _productServices;

        public TSpsController(QLBanDTContext context, ProductServices productServices)
        {
            _context = context;
            _productServices = productServices;
        }

        // GET: TSps
        public async Task<IActionResult> Index()
        {
            var qLBanDTContext = _context.TSp.Include(t => t.MaHangNavigation).Include(t => t.MaTlNavigation);
            ViewBag.TotalProduct = await _productServices.GetTotalProductAsync();
            return View(await qLBanDTContext.ToListAsync());
        }

        // GET: TSps/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TSp == null)
            {
                return NotFound();
            }

            var tSp = await _context.TSp
                .Include(t => t.MaHangNavigation)
                .Include(t => t.MaTlNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (tSp == null)
            {
                return NotFound();
            }

            return View(tSp);
        }

        // GET: TSps/Create
        public IActionResult Create()
        {
            /*var MaTl = (from tSp in _context.TSp
                        join tTl in _context.TTheLoais on tSp.MaTl equals tTl.MaTl
                        select new { tTl.MaTl, tTl.TenTl });*/
            ViewData["MaHang"] = new SelectList(_context.THangs, "MaHang", "MaHang");
            ViewData["MaTl"] = new SelectList(_context.TTheLoais, "MaTl", "TenTl");
            return View();
        }

        // POST: TSps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,TenSp,MaTl,MaHang,DonGiaNhap,DonGiaBan,SoLuong,Anh")] TSp tSp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tSp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHang"] = new SelectList(_context.THangs, "MaHang", "TenHang", tSp.MaHang);
            ViewData["MaTl"] = new SelectList(_context.TTheLoais, "MaTL", "TenTl", tSp.MaTl);
            return View(tSp);
        }

        // GET: TSps/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TSp == null)
            {
                return NotFound();
            }

            var tSp = await _context.TSp.FindAsync(id);
            if (tSp == null)
            {
                return NotFound();
            }
            ViewData["MaHang"] = new SelectList(_context.THangs, "MaHang", "MaHang", tSp.MaHang);
            ViewData["MaTl"] = new SelectList(_context.TTheLoais, "MaTl", "TenTl", tSp.MaTl);
            return View(tSp);
        }

        // POST: TSps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSp,TenSp,MaTl,MaHang,DonGiaNhap,DonGiaBan,SoLuong,Anh")] TSp tSp)
        {
            if (id != tSp.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tSp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TSpExists(tSp.MaSp))
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
            ViewData["MaHang"] = new SelectList(_context.THangs, "MaHang", "MaHang", tSp.MaHang);
            ViewData["MaTl"] = new SelectList(_context.TTheLoais, "MaTl", "TenTl", tSp.MaTl);
            return View(tSp);
        }

        // GET: TSps/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TSp == null)
            {
                return NotFound();
            }

            var tSp = await _context.TSp
                .Include(t => t.MaHangNavigation)
                .Include(t => t.MaTlNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (tSp == null)
            {
                return NotFound();
            }

            return View(tSp);
        }

        // POST: TSps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TSp == null)
            {
                return Problem("Entity set 'QLBanDTContext.TSp'  is null.");
            }
            var tSp = await _context.TSp.FindAsync(id);
            if (tSp != null)
            {
                _context.TSp.Remove(tSp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TSpExists(string id)
        {
          return (_context.TSp?.Any(e => e.MaSp == id)).GetValueOrDefault();
        }
    }
}