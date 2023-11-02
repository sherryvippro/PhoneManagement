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
        public async Task<string> GetTopProductsAsync()
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
                        group chiTietHDB by sp.MaSp into g
                        orderby g.Sum(x => x.Slban) descending
                        select new
                        {
                            masp = g.Key,
                            TotalSales = g.Sum(x => x.Slban),
                            sohdb = g.FirstOrDefault().SoHdb 
                        };
            string result;
            if (query.Select(x => x.masp) != null)
            {
                result =  query.FirstOrDefaultAsync().Result.masp;
                return (string)result;
            }
            else return "";
        }

        public async Task<long> GetMoneyofTopProductsAsync()
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
                        group chiTietHDB by sp.MaSp into g
                        orderby g.Sum(x => x.Slban) descending
                        select new
                        {
                            masp = g.Key,
                            TotalSales = g.Sum(x => x.SoHdbNavigation.TongHdb),
                            sohdb = g.FirstOrDefault().SoHdb
                        };
            long result;
            if (query.Select(x => x.masp) != null)
            {
                result = (long)query.FirstOrDefaultAsync().Result.TotalSales;
                return (long)result;
            }
            else return 0;
        }

        
    }
}
