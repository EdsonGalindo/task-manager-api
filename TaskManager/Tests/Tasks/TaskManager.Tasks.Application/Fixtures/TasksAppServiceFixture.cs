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

        private const string DatabaseName = "TaskManager";

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

            DbContextOptions = new DbContextOptionsBuilder<TasksContext>()
                .UseInMemoryDatabase(databaseName: DatabaseName)
                .Options;

            /*services.AddDbContext<TasksContext>(options =>
            {
                options.UseInMemoryDatabase(DatabaseName);
            });*/

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
