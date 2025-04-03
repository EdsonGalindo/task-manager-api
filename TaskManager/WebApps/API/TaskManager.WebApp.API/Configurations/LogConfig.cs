namespace TaskManager.WebApp.API.Configurations
{
    public static class LogConfig
    {
        public static IServiceCollection AddApplicationLogging(this IServiceCollection services)
        {
            services.AddLogging(logging =>
            {
                logging.AddConsole();
            });

            return services;
        }
    }
}
