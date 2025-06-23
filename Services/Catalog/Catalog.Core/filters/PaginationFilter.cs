namespace Catalog.Core.filters
{
    public class PaginationFilter : BaseFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string[]? OrderBy { get; set; }
    }
}
