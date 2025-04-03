using System;
using System.ComponentModel.DataAnnotations;
using TaskManager.Core.DomainObjects;
using TaskManager.Core.Shared.WebApps.API;
using static TaskManager.Core.Shared.Task.Domain.TaskStatus;

namespace TaskManager.Core.Shared.Tasks.Filter
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
        [DataType(DataType.DateTime, ErrorMessage = "Data inválida. Use o formato yyyy-mm-dd")]
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Filter the tasks by Status
        /// </summary>
        [EnumDataType(typeof(StatusEnum), ErrorMessage = "Status inválido")]
        public StatusEnum? Status { get; set; }

        /// <summary>
        /// Filter tasks by Title
        /// </summary>
        public string? Title { get; set; }
    }
}
