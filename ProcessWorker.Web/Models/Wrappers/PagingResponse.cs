using System.Collections;

namespace ProcessWorker.Web.Models.Wrappers
{
    public class PagingResponse<T> : Response<T>
    {
        public PagingResponse()
        {
            Page = 1;
            PageSize = 10;
        }

        public PagingResponse(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
        
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }
    }
}