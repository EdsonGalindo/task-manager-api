using Microsoft.EntityFrameworkCore;
using TaskManager.Tasks.Domain;

namespace TaskManager.Tasks.Data
{
    public class TasksContext(DbContextOptions<TasksContext> options) : DbContext(options)
    {
        public DbSet<TaskItem> Tasks { get; set; }
    }
}
