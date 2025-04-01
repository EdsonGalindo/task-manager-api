using TaskManager.Core.DomainObjects;
using static TaskManager.Core.Shared.Task.Domain.TaskStatus;

namespace TaskManager.Task.Domain
{
    /// <summary>
    /// Represents a task entity
    /// </summary>
    public class TaskItem : Entity
    {
        public TaskItem(
            string title,
            string? description,
            DateTime? dueDate,
            StatusEnum status)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = status;
        }

        public TaskItem() { }

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Pending;

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
