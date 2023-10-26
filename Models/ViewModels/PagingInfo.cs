namespace Admin.Models.ViewModels
{
    public class PagingInfo
    {
        public int totalItem {  get; set; }
        public int itemsPerPage { get; set; }
        public int currentPage { get; set; }
        public int totalPage => (int)Math.Ceiling((decimal)totalItem / itemsPerPage); 
    }
}
