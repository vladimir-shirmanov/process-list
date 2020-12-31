using System;

namespace ProcessWorker.Web.Models
{
    public class AppProcessModel
    {
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsFinished { get; set; }

        public DateTime? FinishedDate { get; set; }

        public string AuthorUsername { get; set; }
    }
}