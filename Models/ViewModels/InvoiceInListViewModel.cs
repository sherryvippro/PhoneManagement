namespace Admin.Models.ViewModels
{
    public class InvoiceInListViewModel
    {
        public IEnumerable<TChiTietHdn> InvoiceIn { get; set; } = Enumerable.Empty<TChiTietHdn>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
        public IEnumerable<TSp> Products { get; set; } = Enumerable.Empty<TSp>();
    }
}
