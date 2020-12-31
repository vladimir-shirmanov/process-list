namespace ProcessWorker.Entity
{
    public class PagingFilter
    {
        public PagingFilter()
        {
            Page = 1;
            PageSize = 10;
        }
        
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}