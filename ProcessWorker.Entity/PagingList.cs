using System.Collections.Generic;

namespace ProcessWorker.Entity
{
    public class PagingList<T> where T : class
    {
        public int TotalItems { get; set; }

        public List<T> Data { get; set; }
    }
}