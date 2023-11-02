using Admin.Models;

namespace Admin.Services
{
    public class InvoiceServices
    {
        public readonly QLBanDTContext _context;
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
    }
}
