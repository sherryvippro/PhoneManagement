using Admin.Models;
using NuGet.Versioning;

namespace Admin.Services
{
    public class InvoiceServices
    {
        private readonly QLBanDTContext _context;

        public InvoiceServices(QLBanDTContext context)
        {
             _context = context;
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

        public async Task<TChiTietHdn> createInvoiceIn(TChiTietHdn chiTietHdn, string masp)
        {
            /*var tSp = await _context.TSp.FindAsync(masp);
            var Hdn = _context.TChiTietHdns.Where(x => x.MaSp == masp).ToList();
            if (tSp != null)
            {
                tSp.SoLuong += chiTietHdn.Slnhap;
                _context.SaveChanges();
            }
            else
            {
                tSp.MaSp = chiTietHdn.MaSp;
                _context.TSp.AddRange(tSp);
                _context.TChiTietHdns.Add(chiTietHdn);
                _context.SaveChanges();
            }
            return chiTietHdn;*/

            var tSp = await _context.TSp.FindAsync(masp);
            var Hdn = _context.TChiTietHdns.Where(x => x.MaSp == masp).ToList();
            if (tSp != null)
            {
                tSp.SoLuong += chiTietHdn.Slnhap;
                _context.SaveChanges();
            }
            else
            {
                tSp.MaSp = chiTietHdn.MaSp;
                _context.TSp.AddRange(tSp);
                _context.TChiTietHdns.Add(chiTietHdn);
                _context.SaveChanges();
            }
            return chiTietHdn;
        }



    }
}
