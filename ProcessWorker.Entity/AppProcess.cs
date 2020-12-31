using System;

namespace ProcessWorker.Entity
{
    public class AppProcess : BaseEntity
    {
        public AppProcess()
        {
            this.CreatedDate = DateTime.Now;
        }
        
        public string Name { get; set; }

        public DateTime CreatedDate { get; set;  }

        public bool IsFinished { get; set; }

        public DateTime? FinishedDate { get; set; }

        public int AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}