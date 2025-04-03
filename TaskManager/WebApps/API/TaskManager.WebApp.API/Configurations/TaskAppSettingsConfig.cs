using Microsoft.Extensions.Options;
using TaskManager.Core.Shared.WebApps.API;

namespace TaskManager.WebApp.API.Configurations
{
    public static class TaskAppSettingsConfig
    {
        public static IServiceCollection AddTaskAppSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<TaskAppSettings>(configuration.GetSection("TaskManager"));
            services.AddSingleton<ITaskAppSettings>(sp => sp
                .GetRequiredService<IOptions<TaskAppSettings>>().Value);

            return services;
        }
    }
}
