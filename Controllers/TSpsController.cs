using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin.Models;
using Admin.Services;
using PagedList;
using Admin.Models.ViewModels;
using System.Xml.Linq;
using Admin.InputModels;

namespace Admin.Controllers
{
    public class TSpsController : Controller
    {
        private readonly QLBanDTContext _context;
        private readonly ProductServices _productServices;
        /*private readonly ImageServices _imageServices;*/
        public int pageSize = 2;

        public TSpsController(QLBanDTContext context, ProductServices productServices)
        {
            _context = context;
            _productServices = productServices;
            /*_imageServices = imageServices;*/
        }

        // GET: TSps
        public async Task<IActionResult> Index(int productPage = 1)
        {
            /*ar products = _context.TSp.Include(t => t.MaHangNavigation).Include(t => t.MaTlNavigation).ToList();
            return View(products);*/
            /* hàm skip bỏ qua số trang = số trang hiện tại * số lượng item trên mỗi trang
             * hàm take để lấy ra trang tiếp theo với số lượng bản ghi = pagesize*/
            return View(
                new ProductListViewModel
                {
                    Products = _context.TSp.Include(t => t.MaHangNavigation).Include(t => t.MaTlNavigation)
                    .Skip((productPage - 1) * pageSize).Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        itemsPerPage = pageSize,
                        currentPage = productPage,
                        totalItem = _context.TSp.Count(),
                    }
                }
            );

        }
        [HttpPost]
        public async Task<IActionResult> Search(string keyword, int productPage = 1)
        {
            return View(
                new SearchProductListViewModel
                {
                    Products = _context.TSp.Include(t => t.MaHangNavigation).Include(t => t.MaTlNavigation)
                    .Where(t => t.TenSp.Contains(keyword) || t.MaSp.Contains(keyword)
                    || t.MaHangNavigation.TenHang.Contains(keyword) || t.MaTlNavigation.TenTl.Contains(keyword)),
                    /*.Skip((productPage - 1) * pageSize).Take(pageSize),*/
                    SearchPagingInfo = new SearchPagingInfo
                    {
                        itemsPerPage = pageSize,
                        currentPage = productPage,
                        totalItem = _context.TSp.Count(),
                        keyWord = keyword,
                        
                    }
                }
            );

        }
        public async Task<IActionResult> Total()
        {


            ViewBag.TotalProduct = await _productServices.GetTotalProductAsync();
            ViewBag.TotalMoney = await _productServices.GetTotalMoneyAsync();
            ViewBag.Revenue = await _productServices.GetRevenueAsync();
            ViewBag.Profit = await _productServices.GetProfitAsync();

            var MaSp = _productServices.GetTopProductsAsync();
            var TenSp = _context.TSp.Where(t => t.MaSp == MaSp.ToString()).FirstOrDefaultAsync();
            
            var money = _productServices.GetMoneyofTopProductsAsync();
            if (MaSp.ToString() == null)
            {
                ViewBag.TopProduct = "Không có sản phẩm nào";
                ViewBag.TotalSales = "$0";
            }
            else
            {
                ViewBag.TopProduct = TenSp.ToString();
                /*ViewBag.TotalSales = totalSales.ToString();*/
            }


            return View();
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
            ViewData["MaHang"] = new SelectList(_context.THangs, "MaHang", "TenHang");
            ViewData["MaTl"] = new SelectList(_context.TTheLoais, "MaTl", "TenTl");
            return View(tSp);
        }

        // GET: TSps/Create
        public async Task<IActionResult> Create()
        {
            /*var MaTl = (from tSp in _context.TSp
                        join tTl in _context.TTheLoais on tSp.MaTl equals tTl.MaTl
                        select new { tTl.MaTl, tTl.TenTl });*/
            var query = from c in _context.THoaDonNhaps
                        join c1 in _context.TChiTietHdns on c.SoHdn equals c1.SoHdn
                        select new
                        {
                            sohdn = c.SoHdn,
                            masp = c1.MaSp
                        };
            ViewData["MaSp"] = new SelectList(query.Select(t => t.masp).ToList());
            ViewData["MaHang"] = new SelectList(_context.THangs, "MaHang", "TenHang");
            ViewData["MaTl"] = new SelectList(_context.TTheLoais, "MaTl", "TenTl");

            return View();
        }

        // POST: TSps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,TenSp,MaTl,MaHang,DonGiaNhap,DonGiaBan,SoLuong,Anh")] TSp tSp/*, InputProducts inputProducts*/)
        {
            /*var fileName = await _imageServices.SaveFileAsync(inputProducts.Anh);
            tSp.Anh = fileName;*/
            if (ModelState.IsValid)
            {

                var product = _productServices.CreateProductAsync(tSp, tSp.MaSp);
                return RedirectToAction(nameof(Index));

            }
            var query = from c in _context.THoaDonNhaps
                        join c1 in _context.TChiTietHdns on c.SoHdn equals c1.SoHdn
                        select new
                        {
                            sohdb = c.SoHdn,
                            masp = c1.MaSp
                        };
            ViewData["MaSp"] = new SelectList(query.Select(t => t.masp).ToList());
            ViewData["MaHang"] = new SelectList(_context.THangs, "MaHang", "TenHang", tSp.MaHang);
            ViewData["MaTl"] = new SelectList(_context.TTheLoais, "MaTL", "TenTl", tSp.MaTl);
            return View(tSp);
        }
        /*public async Task<IActionResult> PostProduct([FromForm]InputProducts inputProducts)
        {
            var product = await _productServices.CreateProductAsync(inputProducts);
            return CreatedAtAction(nameof(Create), new { id = product.MaSp } , product);
        }
*/
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
            ViewData["MaHang"] = new SelectList(_context.THangs, "MaHang", "TenHang", tSp.MaHang);
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
            ViewData["MaHang"] = new SelectList(_context.THangs, "MaHang", "TenHang", tSp.MaHang);
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
            var tChiTietHDN = await _context.TChiTietHdns.Where(x => x.MaSp == id).ToListAsync();
            if (tSp != null)
            {
                _context.TChiTietHdns.RemoveRange(tChiTietHDN);
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
