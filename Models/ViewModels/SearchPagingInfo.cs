namespace Admin.Models.ViewModels
{
    public class SearchPagingInfo
    {
        public int totalItem { get; set; }
        public int itemsPerPage { get; set; }
        public int currentPage { get; set; }
        public int totalPage => (int)Math.Ceiling((decimal)totalItem / itemsPerPage);
        public string keyWord { get; set; }
    }
}
