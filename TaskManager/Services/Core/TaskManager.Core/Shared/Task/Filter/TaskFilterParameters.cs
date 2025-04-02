using System;
using TaskManager.Core.DomainObjects;
using TaskManager.Core.Shared.WebApps.API;
using static TaskManager.Core.Shared.Task.Domain.TaskStatus;

namespace TaskManager.Core.Shared.Task.Filter
{
    /// <summary>
    /// Parameters for filtering get Tasks results and limiting 
    /// the amount of registers returned by pagination
    /// </summary>
    public class TaskFilterParameters : FilterParameters
    {
        public TaskFilterParameters(ITaskAppSettings taskAppSettings)
            : base(taskAppSettings.Pagination) { }

        public TaskFilterParameters() { }

        /// <summary>
        /// Filter tasks by Due Date
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Filter the tasks by Status
        /// </summary>
        public StatusEnum? Status { get; set; }

        /// <summary>
        /// Filter tasks by Title
        /// </summary>
        public string? Title { get; set; }
    }
}
