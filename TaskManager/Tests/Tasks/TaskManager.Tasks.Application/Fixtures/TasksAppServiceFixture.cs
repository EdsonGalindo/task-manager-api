using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Tasks.Application.AutoMapper;
using TaskManager.Tasks.Application.Services;
using TaskManager.Tasks.Data;
using TaskManager.Tasks.Data.Repository;
using TaskManager.Tasks.Domain;

namespace TaskManager.Tasks.Application.Tests.Fixtures
{
    public class TasksAppServiceFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; }

        public TasksAppServiceFixture()
        {
            var services = new ServiceCollection();

            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<ITasksAppservice, TasksAppService>();
            services.AddScoped<TasksContext>();
            services.AddAutoMapper(configs =>
            {
                configs.AddProfile<DomainToViewModelMappingProfile>();
                configs.AddProfile<ViewModelToDomainMappingProfile>();
            });

            services.AddDbContext<TasksContext>(options =>
            {
                options.UseInMemoryDatabase("TaskManager");
            });

            ServiceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
