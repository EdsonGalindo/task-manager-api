using Microsoft.EntityFrameworkCore;
using TaskManager.Task.Domain;

namespace TaskManager.Task.Data
{
    public class TaskContext(DbContextOptions<TaskContext> options) : DbContext(options)
    {
        public DbSet<TaskItem> Tasks { get; set; }
    }
}
