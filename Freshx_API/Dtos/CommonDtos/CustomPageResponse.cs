using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Freshx_API.Dtos.CommonDtos
{
    public class CustomPageResponse <T>
    {
        public T Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public CustomPageResponse(T data, int pageNumber, int pageSize, int totalRecords)
        {
            Items = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            HasNextPage = PageNumber < TotalPages;
            HasPreviousPage = PageNumber > 1;
        }
    }
}
