using Admin.Models;
using Microsoft.EntityFrameworkCore;

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
            if(sohdn.Count() != 0)
            {
                SoHDN = "HD" + month.ToString() + day.ToString() + rd.Next(1000, 9999);

            }
            return (string)SoHDN;
        }
    }
}
