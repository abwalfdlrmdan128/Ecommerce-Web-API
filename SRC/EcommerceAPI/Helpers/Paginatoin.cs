
namespace EcommerceAPI.Helpers
{
    public class Paginatoin<T>where T : class
    {
        public Paginatoin(int pageNumber, int pageSize, int totalCount, IEnumerable<T> items)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items;
        }

        public int PageNumber { get; set; }
        public int PageSize {  get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }

    }
}
