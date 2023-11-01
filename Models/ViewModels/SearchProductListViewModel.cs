namespace Admin.Models.ViewModels
{
    public class SearchProductListViewModel
    {
        public IEnumerable<TSp> Products { get; set; } = Enumerable.Empty<TSp>();
        public SearchPagingInfo SearchPagingInfo { get; set; } = new SearchPagingInfo();
    }
}
