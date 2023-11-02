using Admin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Linq;

namespace Admin.Services
{
    public class ProductServices
    {
        private readonly QLBanDTContext _context;

        public ProductServices(QLBanDTContext context)
        {
            _context = context;
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
        public async Task<double> GetProductSold()
        {
            var productSold = await _context.TChiTietHdbs.Include(t => t.SoHdbNavigation).SumAsync(x => x.Slban);
            return (double)productSold;
        }
        /*public async Task<TChiTietHdb> GetTopProducts()
        {
            *//*var day =  _context.THoaDonBans.Select(x => x.NgayBan).ToList();
            foreach(var i in day)
            {
                DateTime.Parse(i);
            }*//*
            var query = from sp in _context.TSp
                        join chiTietHDB in _context.TChiTietHdbs on sp.MaSp equals chiTietHDB.MaSp
                        join hoaDonBan in _context.THoaDonBans on chiTietHDB.SoHdb equals hoaDonBan.SoHdb
                        *//*where DateTime.Parse(hoaDonBan.NgayBan).Year == DateTime.Now.Year*//*
                        group chiTietHDB by sp.MaSp into g
                        orderby g.Sum(x => x.Slban) descending
                        select new
                        {
                            masp = g.Key,
                            TotalSales = g.Sum(x => x.Slban)
                        };
            var result = query.FirstOrDefault();

            return result;
        }*/

<<<<<<< HEAD
=======
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

>>>>>>> bc875c96f66c76dff785ed1380281fb5df082a71
    }
}