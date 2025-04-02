using TaskManager.Tasks.Application.Services;
using TaskManager.Tasks.Data;
using TaskManager.Tasks.Data.Repository;
using TaskManager.Tasks.Domain;

namespace TaskManager.WebApp.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<ITasksAppservice, TasksAppService>();
            services.AddScoped<TasksContext>();
                        
            return services;
        }
    }
}
