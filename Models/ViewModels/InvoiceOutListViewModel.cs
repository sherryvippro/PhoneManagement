namespace Admin.Models.ViewModels
{
    public class InvoiceOutListViewModel
    {
        public IEnumerable<TChiTietHdb> InvoiceOut { get; set; } = Enumerable.Empty<TChiTietHdb>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
