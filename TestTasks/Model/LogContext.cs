using Microsoft.EntityFrameworkCore;

namespace TestTasks.Model
{
    public class LogContext : DbContext
    {
        public LogContext(DbContextOptions<LogContext> options) : base(options)
        { 
        }

        public DbSet<Log> Log { get; set; }
    }
}
