using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Tasks.Application.AutoMapper;
using TaskManager.Tasks.Application.Services;
using TaskManager.Tasks.Application.ViewModels;
using TaskManager.Tasks.Data;
using TaskManager.Tasks.Data.Repository;
using TaskManager.Tasks.Domain;

namespace TaskManager.Tasks.Application.Tests.Fixtures
{
    public class TasksAppServiceFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; }
        public DbContextOptions<TasksContext> DbContextOptions { get; }
        public TaskItemViewModel ExistentTaskItem { get; set; }

        private string DatabaseName = "TaskManager";

        public TasksAppServiceFixture()
        {
            DatabaseName += Random.Shared.Next();

            var services = new ServiceCollection();

            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<ITasksAppservice, TasksAppService>();
            services.AddScoped<TasksContext>();
            services.AddAutoMapper(configs =>
            {
                configs.AddProfile<DomainToViewModelMappingProfile>();
                configs.AddProfile<ViewModelToDomainMappingProfile>();
            });

            DbContextOptions = new DbContextOptionsBuilder<TasksContext>()
                .UseInMemoryDatabase(databaseName: DatabaseName)
                .Options;

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
