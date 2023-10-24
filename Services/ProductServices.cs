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
    }
}
