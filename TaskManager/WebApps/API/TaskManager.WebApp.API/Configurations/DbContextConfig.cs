using Microsoft.EntityFrameworkCore;
using TaskManager.Tasks.Data;

namespace TaskManager.WebApp.API.Configurations
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddTasksInMemoryDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TasksContext>(options =>
            {
                options.UseInMemoryDatabase("TaskManager");
            });
            return services;
        }
    }
}
