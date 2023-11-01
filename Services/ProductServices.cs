using Admin.InputModels;
using Admin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Linq;

namespace Admin.Services
{
    public class ProductServices
    {
        private readonly QLBanDTContext _context;
        private readonly ImageServices _imageServices;

        public ProductServices(QLBanDTContext context, ImageServices imageServices)
        {
            _context = context;
            _imageServices = imageServices;
        }
        public async Task<TSp> CreateProductAsync(TSp tSp,string ID)
        {
            var query = _context.TSp.Find(ID);
            if(query != null)
            {
                query.TenSp = tSp.TenSp;
                query.SoLuong += tSp.SoLuong;
                query.DonGiaBan = tSp.DonGiaBan;
                query.DonGiaNhap = tSp.DonGiaNhap;
                query.Anh = tSp.Anh;
                _context.SaveChanges();
            }
            else
            {
                _context.Add(tSp);
                _context.SaveChanges();
            }
            return tSp;
        }

        public async Task<int> GetTotalProductAsync()
        {
            var total = await _context.TSp.SumAsync(x => x.SoLuong);
            return (int)total;
        }
        public async Task<double> GetTotalMoneyAsync()
        {
            var money = await _context.THoaDonNhaps.SumAsync(x => x.TongHdn);
            return (double)money;
        }
        public async Task<double> GetRevenueAsync()
        {
            var revenue = await _context.THoaDonBans.SumAsync(x => x.TongHdb);
            return (double)revenue;
        }
        public async Task<double> GetProfitAsync()
        {
            var profit = await _context.THoaDonBans.SumAsync(x => x.TongHdb) - await _context.THoaDonNhaps.SumAsync(x => x.TongHdn);
            return (double)profit;
        }
        public async Task<double> GetProductSoldAsync()
        {
            var productSold = await _context.TChiTietHdbs.Include(t => t.SoHdbNavigation).SumAsync(x => x.Slban);
            return (double)productSold;
        }
        public async Task<TChiTietHdb> GetTopProductsAsync()
        {
            /*var day =  _context.THoaDonBans.Select(x => x.NgayBan).ToList();
            foreach(var i in day)
            {
                DateTime.Parse(i);
            }*/
            var query = from sp in _context.TSp
                        join chiTietHDB in _context.TChiTietHdbs on sp.MaSp equals chiTietHDB.MaSp
                        join hoaDonBan in _context.THoaDonBans on chiTietHDB.SoHdb equals hoaDonBan.SoHdb
                        where hoaDonBan.NgayBan.Value.Year == DateTime.Now.Year
                        /*group chiTietHDB by sp.MaSp into g
                        orderby g.Sum(x => x.Slban) descending*/
                        select new
                        {
                            masp = sp.MaSp,
                            TotalSales = chiTietHDB.Slban,
                            sohdb = chiTietHDB.SoHdb 
                        };
            var result = await query.FirstOrDefaultAsync();
            return new TChiTietHdb() { MaSp = result.masp, SoHdb = result.sohdb};
        }

        public async Task<THoaDonBan> GetMoneyofTopProductsAsync()
        {
            /*var day =  _context.THoaDonBans.Select(x => x.NgayBan).ToList();
            foreach(var i in day)
            {
                DateTime.Parse(i);
            }*/
            var query = from sp in _context.TSp
                        join chiTietHDB in _context.TChiTietHdbs on sp.MaSp equals chiTietHDB.MaSp
                        join hoaDonBan in _context.THoaDonBans on chiTietHDB.SoHdb equals hoaDonBan.SoHdb
                        where hoaDonBan.NgayBan.Value.Year == DateTime.Now.Year
                        /*group chiTietHDB by sp.MaSp into g
                        orderby g.Sum(x => x.Slban) descending*/
                        select new
                        {
                            masp = sp.MaSp,
                            TotalSales = chiTietHDB.Slban,
                            sohdb = chiTietHDB.SoHdb
                        };
            var result = await query.FirstOrDefaultAsync();
            return new THoaDonBan() { TongHdb = result.TotalSales, SoHdb = result.sohdb };
        }

        public async Task<string> GenerateSHDNAsync()
        {
            Random rd = new Random();
            int rdNumber = rd.Next(1000, 9999);
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            var SoHDN = "HD" + month.ToString() + day.ToString() + rdNumber.ToString();
            var sohdn = _context.THoaDonNhaps.Where(predicate: t => t.SoHdn == SoHDN)
                                                   .Select(t => t.SoHdn)
                                                   .ToList();
            if (sohdn.Count() != 0)
            {
                SoHDN = "HD" + month.ToString() + day.ToString() + rd.Next(1000, 9999);

            }
            return (string)SoHDN;
        }
    }
}
