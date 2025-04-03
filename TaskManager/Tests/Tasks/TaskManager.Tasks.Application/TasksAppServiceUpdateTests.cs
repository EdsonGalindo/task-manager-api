using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.Shared.Tasks.Constants;
using TaskManager.Tasks.Application.Services;
using TaskManager.Tasks.Application.Tests.Fixtures;
using TaskManager.Tasks.Application.ViewModels;
using TaskManager.Tasks.Data;
using TaskManager.Tasks.Data.Repository;
using TaskManager.Tasks.Domain;
using static TaskManager.Core.Shared.Task.Domain.TaskStatus;

namespace TaskManager.Tasks.Application
{
    public class TasksAppServiceUpdateTests : IClassFixture<TasksAppServiceFixture>
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

        public TasksAppServiceUpdateTests(TasksAppServiceFixture tasksAppServiceFixture)
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
        public void UpdateTaskAsync_WhenIdInvalid_ShouldFail()
        {
            _taskItem.Id = 0;

            Assert.ThrowsAsync<ArgumentException>(nameof(TaskItemViewModel.Id), () =>
                _tasksAppService.UpdateTaskAsync(_taskItem));
        }

        [Fact]
        public void UpdateTaskAsync_WhenIdValid_ShouldSucceed()
        {
            _taskItem.Id = 1;

            Assert.True(_tasksAppService.UpdateTaskAsync(_taskItem).Result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void UpdateTaskAsync_WhenTitleNotInformed_ShouldFail(string? taskTitle)
        {
            _taskItem.Title = taskTitle;

            Assert.ThrowsAsync<ArgumentException>(nameof(TaskItemViewModel.Title), () =>
                _tasksAppService.UpdateTaskAsync(_taskItem));
        }

        [Fact]
        public void UpdateTaskAsync_WhenTitleInformed_ShouldSucceed()
        {
            Assert.NotNull(_tasksAppService.UpdateTaskAsync(_taskItem));
        }

        [Fact]
        public void UpdateTaskAsync_WhenTitleLengthExceedsMaxLength_ShouldFail()
        {
            _taskItem.Title = new string('a', TasksConstants.TaskTitleMaxLength + 1);

            Assert.ThrowsAsync<ArgumentException>(nameof(TaskItemViewModel.Title), () =>
                _tasksAppService.UpdateTaskAsync(_taskItem));
        }

        [Fact]
        public void UpdateTaskAsync_WhenTitleLengthEqualMaxLength_ShouldSucceed()
        {
            _taskItem.Title = new string('a', TasksConstants.TaskTitleMaxLength);

            Assert.NotNull(_tasksAppService.UpdateTaskAsync(_taskItem));
        }

        [Fact]
        public void UpdateTaskAsync_WhenTitleLengthLessMaxLength_ShouldSucceed()
        {
            _taskItem.Title = new string('a', TasksConstants.TaskTitleMaxLength - 50);

            Assert.NotNull(_tasksAppService.UpdateTaskAsync(_taskItem));
        }

        [Fact]
        public void UpdateTaskAsync_WhenDescriptionLengthExceedsMaxLength_ShouldFail()
        {
            _taskItem.Description = new string('a', TasksConstants.TaskDescriptionMaxLength + 1);

            Assert.ThrowsAsync<ArgumentException>(nameof(TaskItem.Description), () =>
                _tasksAppService.UpdateTaskAsync(_taskItem));
        }

        [Fact]
        public void UpdateTaskAsync_WhenDescriptionLengthEqualMaxLength_ShouldSucceed()
        {
            _taskItem.Description = new string('a', TasksConstants.TaskDescriptionMaxLength);

            Assert.NotNull(_tasksAppService.UpdateTaskAsync(_taskItem));
        }

        [Fact]
        public void UpdateTaskAsync_WhenDescriptionLengthLessMaxLength_ShouldSucceed()
        {
            _taskItem.Description = new string('a', TasksConstants.TaskDescriptionMaxLength - 50);

            Assert.NotNull(_tasksAppService.UpdateTaskAsync(_taskItem));
        }

        [Fact]
        public void UpdateTaskAsync_WhenInvalidStatus_ShouldFail()
        {
            _taskItem.Status = (StatusEnum)999;

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(nameof(TaskItem.Status), () =>
                _tasksAppService.UpdateTaskAsync(_taskItem));
        }

        [Fact]
        public void UpdateTaskAsync_WhenValidStatus_ShouldSucceed()
        {
            _taskItem.Status = (StatusEnum)1;

            _taskDueDate = null;
            Assert.NotNull(_tasksAppService.UpdateTaskAsync(_taskItem));
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