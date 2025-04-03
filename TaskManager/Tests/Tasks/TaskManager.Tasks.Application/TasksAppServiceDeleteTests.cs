using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Tasks.Application.Services;
using TaskManager.Tasks.Application.Tests.Fixtures;
using TaskManager.Tasks.Application.ViewModels;
using TaskManager.Tasks.Data;
using TaskManager.Tasks.Data.Repository;
using TaskManager.Tasks.Domain;
using static TaskManager.Core.Shared.Task.Domain.TaskStatus;

namespace TaskManager.Tasks.Application
{
    public class TasksAppServiceDeleteTests : IClassFixture<TasksAppServiceFixture>
    {
        private readonly ITasksAppservice _tasksAppService;
        private int _taskId;
        private string? _taskTitle;
        private string? _taskDescription;
        private DateTime? _taskDueDate;
        private StatusEnum? _taskStatus;
        private TaskItemViewModel _taskItem;
        private DbContextOptions<TasksContext> _tasksDbContextOptions;
        private readonly IMapper _mapper;

        public TasksAppServiceDeleteTests(TasksAppServiceFixture tasksAppServiceFixture)
        {
            #region Set the App Service and its dependencies
            var tasksDbContext = new TasksContext(tasksAppServiceFixture.DbContextOptions);
            var tasksRepository = new TasksRepository(tasksDbContext);

            _tasksAppService = new TasksAppService(tasksRepository,
                tasksAppServiceFixture.ServiceProvider.GetRequiredService<IMapper>());
            #endregion

            #region Set fixture shared properties
            _tasksDbContextOptions = tasksAppServiceFixture.DbContextOptions;

            _mapper = tasksAppServiceFixture
                .ServiceProvider.GetRequiredService<IMapper>();
            #endregion

            #region Set Test TaskItem
            _taskId = 1;
            _taskTitle = "Test task";
            _taskDescription = null;
            _taskDueDate = null;
            _taskStatus = StatusEnum.Pending;

            #region Await remove the existing Database to avoid conflicts
            Thread.Sleep(10);
            #endregion

            _taskItem = CreateTaskItemAsync().Result;
            #endregion
        }

        [Fact]
        public async Task DeleteTaskAsync_WhenIdInvalid_ShouldFail()
        {
            try
            {
                _ = await _tasksAppService.DeleteTaskAsync(0);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.True(ex is ArgumentException);
            }
        }

        [Fact]
        public async Task DeleteTaskAsync_WhenIdValid_ShouldSucceed()
        {
            Assert.True(await _tasksAppService.DeleteTaskAsync(1));
        }

        [Fact]
        public async Task DeleteTaskAsync_WhenTaskNotExists_ShouldFail()
        {
            try
            {
                _ = await _tasksAppService.DeleteTaskAsync(999);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.True(ex is ArgumentException);
            }
        }

        private async Task<TaskItemViewModel> CreateTaskItemAsync()
        {
            var taskItem = new TaskItemViewModel
            {
                Id = _taskId,
                Title = _taskTitle,
                Description = _taskDescription,
                DueDate = _taskDueDate,
                Status = _taskStatus
            };

            using (var tasksDbContext = new TasksContext(_tasksDbContextOptions))
            {
                await tasksDbContext.Database.EnsureDeletedAsync();
                await tasksDbContext.Database.EnsureCreatedAsync();
                await tasksDbContext.Tasks.AddAsync(_mapper.Map<TaskItem>(taskItem));                
                await tasksDbContext.SaveChangesAsync();
            }

            return taskItem;
        }
    }
}