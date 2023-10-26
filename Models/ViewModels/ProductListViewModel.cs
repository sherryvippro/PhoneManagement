namespace Admin.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<TSp> Products { get; set; } = Enumerable.Empty<TSp>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
