using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskManager.WebApp.API.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection SetSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Title = $"API - Task Manager - {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = "API para gerencimento de tarefas"
                    });
                }

                options.OperationFilter<SwaggerDefaultValues>();
                options.EnableAnnotations();
            });

            services.AddTransient<SwaggerDefaultValues>();

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger(options => {
                    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
                });

            app.UseSwaggerUI(
                options =>
                {
                    var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });

            return app;
        }

        public class SwaggerDefaultValues : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null)
                {
                    return;
                }

                foreach (var parameter in operation.Parameters)
                {
                    var description = context.ApiDescription
                        .ParameterDescriptions
                        .First(p => p.Name == parameter.Name);

                    var routeInfo = description.RouteInfo;

                    operation.Deprecated = OpenApiOperation.DeprecatedDefault;

                    parameter.Description ??= description.ModelMetadata?.Description;

                    if (routeInfo == null)
                    {
                        continue;
                    }

                    if (parameter.In != ParameterLocation.Path && parameter.Schema.Default == null)
                    {
                        parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue?.ToString());
                    }

                    parameter.Required |= !routeInfo.IsOptional;
                }
            }
        }
    }
}
