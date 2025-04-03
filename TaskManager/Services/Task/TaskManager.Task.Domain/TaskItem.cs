using TaskManager.Core.DomainObjects;
using TaskManager.Core.Shared.Tasks.Constants;
using static TaskManager.Core.Shared.Task.Domain.TaskStatus;

namespace TaskManager.Tasks.Domain
{
    /// <summary>
    /// Represents a task entity
    /// </summary>
    public class TaskItem : Entity
    {
        private string _title = string.Empty;
        private string? _description { get; set; }
        private StatusEnum? _status { get; set; } = StatusEnum.Pending;

        public TaskItem(
            string title,
            string? description,
            DateTime? dueDate,
            StatusEnum? status)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = status;
        }

        public TaskItem() { }

        public string Title 
        {
            get => _title;
            set
            {
                var propertyName = nameof(Title);

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(
                        TasksConstants.TaskTitleNotInformed,
                        propertyName);
                }

                if (value.Length > TasksConstants.TaskTitleMaxLength)
                {
                    throw new ArgumentException(
                        TasksConstants.GetMaxLengthErrorMessage(
                            propertyName,
                            TasksConstants.TaskTitleMaxLength),
                        propertyName);
                }

                _title = value;
            }
        }

        public StatusEnum? Status
        {
            get => _status;
            set
            {
                if (value != null && !Enum.IsDefined(typeof(StatusEnum), value))
                {
                    throw new ArgumentOutOfRangeException(nameof(Status), TasksConstants.TaskStatusIsInvalid);
                }

                _status = value;
            }
        }

        public string? Description
        {
            get => _description;
            set
            {
                var propertyName = nameof(Description);

                if (value != null && value.Length > TasksConstants.TaskDescriptionMaxLength)
                {
                    throw new ArgumentException(
                        TasksConstants.GetMaxLengthErrorMessage(
                            propertyName,
                            TasksConstants.TaskDescriptionMaxLength),
                        propertyName);
                }

                _description = value;
            }
        }

        public DateTime? DueDate { get; set; }



        /// <summary>
        /// Marks the task as pending.
        /// </summary>
        public void MarkAsPending()
        {
            Status = StatusEnum.Pending;
        }

        /// <summary>
        /// Marks the task as in progress.
        /// </summary>
        public void MarkAsInProgress()
        {
            Status = StatusEnum.InProgress;
        }

        /// <summary>
        /// Marks the task as completed.
        /// </summary>
        public void MarkAsCompleted()
        {
            Status = StatusEnum.Completed;
        }
    }
}
