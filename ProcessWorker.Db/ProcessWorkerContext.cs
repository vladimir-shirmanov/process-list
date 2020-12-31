using Microsoft.EntityFrameworkCore;
using ProcessWorker.Entity;

namespace ProcessWorker.Db
{
    public sealed class ProcessWorkerContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<AppProcess> AppProcesses { get; set; }

        public ProcessWorkerContext(DbContextOptions<ProcessWorkerContext> options)
            : base(options)
        {
        }
    }
}