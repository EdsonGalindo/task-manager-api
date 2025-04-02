namespace TaskManager.WebApp.API.Configurations
{
    public static class AutoMapperProfilesConfig
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
