using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.Shared.Tasks.Constants;
using TaskManager.Tasks.Application.Services;
using TaskManager.Tasks.Application.Tests.Fixtures;
using TaskManager.Tasks.Application.ViewModels;
using TaskManager.Tasks.Data.Repository;
using TaskManager.Tasks.Data;
using TaskManager.Tasks.Domain;
using static TaskManager.Core.Shared.Task.Domain.TaskStatus;

namespace TaskManager.Tasks.Application
{
    public class TasksAppServiceAddTests : IClassFixture<TasksAppServiceFixture>
    {
        private readonly ITasksAppservice _tasksAppService;
        private string? _taskTitle;
        private string? _taskDescription;
        private DateTime? _taskDueDate;
        private StatusEnum? _taskStatus;

        public TasksAppServiceAddTests(TasksAppServiceFixture tasksAppServiceFixture)
        {
            #region Set the App Service and its dependencies
            var tasksDbContext = new TasksContext(tasksAppServiceFixture.DbContextOptions);
            var tasksRepository = new TasksRepository(tasksDbContext);

            _tasksAppService = new TasksAppService(tasksRepository,
                tasksAppServiceFixture.ServiceProvider.GetRequiredService<IMapper>());
            #endregion

            _taskTitle = "Test task";
            _taskDescription = string.Empty;
            _taskDueDate = null;
            _taskStatus = null;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void AddTaskAsync_WhenTitleNotInformed_ShouldFail(string? taskTitle)
        {
            _taskTitle = taskTitle;

            Assert.ThrowsAsync<ArgumentException>(nameof(TaskItemViewModel.Title), () =>
                _tasksAppService.AddTaskAsync(CreateTaskItem()));
        }

        [Fact]
        public void AddTaskAsync_WhenTitleInformed_ShouldSucceed()
        {
            Assert.NotNull(_tasksAppService.AddTaskAsync(CreateTaskItem()));
        }

        [Fact]
        public void AddTaskAsync_WhenTitleLengthExceedsMaxLength_ShouldFail()
        {
            _taskTitle = new string('a', TasksConstants.TaskTitleMaxLength + 1);

            Assert.ThrowsAsync<ArgumentException>(nameof(TaskItemViewModel.Title), () =>
                _tasksAppService.AddTaskAsync(CreateTaskItem()));
        }

        [Fact]
        public void AddTaskAsync_WhenTitleLengthEqualMaxLength_ShouldSucceed()
        {
            _taskTitle = new string('a', TasksConstants.TaskTitleMaxLength);

            Assert.NotNull(_tasksAppService.AddTaskAsync(CreateTaskItem()));
        }

        [Fact]
        public void AddTaskAsync_WhenTitleLengthLessMaxLength_ShouldSucceed()
        {
            _taskTitle = new string('a', TasksConstants.TaskTitleMaxLength - 50);

            Assert.NotNull(_tasksAppService.AddTaskAsync(CreateTaskItem()));
        }

        [Fact]
        public void AddTaskAsync_WhenDescriptionLengthExceedsMaxLength_ShouldFail()
        {
            _taskDescription = new string('a', TasksConstants.TaskDescriptionMaxLength + 1);

            Assert.ThrowsAsync<ArgumentException>(nameof(TaskItem.Description), () =>
                _tasksAppService.AddTaskAsync(CreateTaskItem()));
        }

        [Fact]
        public void AddTaskAsync_WhenDescriptionLengthEqualMaxLength_ShouldSucceed()
        {
            _taskDescription = new string('a', TasksConstants.TaskDescriptionMaxLength);

            Assert.NotNull(_tasksAppService.AddTaskAsync(CreateTaskItem()));
        }

        [Fact]
        public void AddTaskAsync_WhenDescriptionLengthLessMaxLength_ShouldSucceed()
        {
            _taskDescription = new string('a', TasksConstants.TaskDescriptionMaxLength - 50);

            Assert.NotNull(_tasksAppService.AddTaskAsync(CreateTaskItem()));
        }

        [Fact]
        public void AddTaskAsync_WhenInvalidStatus_ShouldFail()
        {
            _taskStatus = (StatusEnum)999;

            _taskDueDate = null;
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(nameof(TaskItem.Status), () =>
                _tasksAppService.AddTaskAsync(CreateTaskItem()));
        }

        [Fact]
        public void AddTaskAsync_WhenValidStatus_ShouldSucceed()
        {
            _taskStatus = (StatusEnum)1;

            _taskDueDate = null;
            Assert.NotNull(_tasksAppService.AddTaskAsync(CreateTaskItem()));
        }

        private TaskItemViewModel CreateTaskItem()
        {
            return new TaskItemViewModel
            {
                Title = _taskTitle,
                Description = _taskDescription,
                DueDate = _taskDueDate,
                Status = _taskStatus
            };
        }
    }
}