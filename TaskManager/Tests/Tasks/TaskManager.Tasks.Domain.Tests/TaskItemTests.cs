using TaskManager.Core.Shared.Tasks.Constants;
using static TaskManager.Core.Shared.Task.Domain.TaskStatus;

namespace TaskManager.Tasks.Domain.Tests
{
    public class TaskItemTests
    {
        private string? _taskTitle;
        private string? _taskDescription;
        private DateTime? _taskDueDate;
        private StatusEnum? _taskStatus;

        public TaskItemTests() 
        {
            _taskTitle = "Test task";
            _taskDescription = string.Empty;
            _taskDueDate = null;
            _taskStatus = null;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateTask_WhenTitleNotInformed_ShouldFail(string? taskTitle)
        {
            Assert.Throws<ArgumentException>(nameof(TaskItem.Title), () => 
                CreateTask(taskTitle, _taskDescription, _taskDueDate, _taskStatus));
        }

        [Fact]
        public void CreateTask_WhenTitleInformed_ShouldSucceed()
        {
            Assert.NotNull(CreateTask(_taskTitle, _taskDescription, _taskDueDate, _taskStatus));
        }

        [Fact]
        public void CreateTask_WhenTitleLengthExceedsMaxLength_ShouldFail()
        {
            _taskTitle = new string('a', TasksConstants.TaskTitleMaxLength + 1);

            Assert.Throws<ArgumentException>(nameof(TaskItem.Title), () =>
                CreateTask(_taskTitle, _taskDescription, _taskDueDate, _taskStatus));
        }

        [Fact]
        public void CreateTask_WhenTitleLengthEqualMaxLength_ShouldSucceed()
        {
            _taskTitle = new string('a', TasksConstants.TaskTitleMaxLength);

            Assert.NotNull(CreateTask(_taskTitle, _taskDescription, _taskDueDate, _taskStatus));
        }

        [Fact]
        public void CreateTask_WhenTitleLengthLessMaxLength_ShouldSucceed()
        {
            _taskTitle = new string('a', TasksConstants.TaskTitleMaxLength - 50);

            Assert.NotNull(CreateTask(_taskTitle, _taskDescription, _taskDueDate, _taskStatus));
        }

        [Fact]
        public void CreateTask_WhenDescriptionLengthExceedsMaxLength_ShouldFail()
        {
            _taskDescription = new string('a', TasksConstants.TaskDescriptionMaxLength + 1);

            Assert.Throws<ArgumentException>(nameof(TaskItem.Description), () =>
                CreateTask(_taskTitle, _taskDescription, _taskDueDate, _taskStatus));
        }

        [Fact]
        public void CreateTask_WhenDescriptionLengthEqualMaxLength_ShouldSucceed()
        {
            _taskDescription = new string('a', TasksConstants.TaskDescriptionMaxLength);

            Assert.NotNull(CreateTask(_taskTitle, _taskDescription, _taskDueDate, _taskStatus));
        }

        [Fact]
        public void CreateTask_WhenDescriptionLengthLessMaxLength_ShouldSucceed()
        {
            _taskDescription = new string('a', TasksConstants.TaskDescriptionMaxLength - 50);

            Assert.NotNull(CreateTask(_taskTitle, _taskDescription, _taskDueDate, _taskStatus));
        }

        [Fact]
        public void CreateTask_WhenInvalidStatus_ShouldFail()
        {
            _taskStatus = (StatusEnum)999;

            _taskDueDate = null;
            Assert.Throws<ArgumentOutOfRangeException>(nameof(TaskItem.Status), () =>
                CreateTask(_taskTitle, _taskDescription, _taskDueDate, _taskStatus));
        }

        [Fact]
        public void CreateTask_WhenValidStatus_ShouldSucceed()
        {
            _taskStatus = (StatusEnum)1;

            _taskDueDate = null;
            Assert.NotNull(CreateTask(_taskTitle, _taskDescription, _taskDueDate, _taskStatus));
        }

        private static TaskItem CreateTask(
            string? title,
            string? description,
            DateTime? dueDate,
            StatusEnum? status)
        {
            return new TaskItem(title, description, dueDate, status);
        }
    }
}