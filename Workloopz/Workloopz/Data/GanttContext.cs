using Microsoft.EntityFrameworkCore;

namespace Workloopz.Data
{
	public class GanttContext : DbContext
	{
        public GanttContext(DbContextOptions<GanttContext> options)
       : base(options)
        {
        }
        public DbSet<Task> Tasks { get; set; } = null;
        public DbSet<Link> Links { get; set; } = null;
    }
}
