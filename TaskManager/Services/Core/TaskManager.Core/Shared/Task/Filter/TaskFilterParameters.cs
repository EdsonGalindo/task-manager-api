using TaskManager.Core.DomainObjects;
using TaskManager.Core.Shared.WebApps.API;

namespace TaskManager.Core.Shared.Task.Filter
{
    /// <summary>
    /// Parameters for filtering get Tasks results and limiting 
    /// the amount of registers returned by pagination
    /// </summary>
    public class TaskFilterParameters : FilterParameters
    {
        public TaskFilterParameters(TaskAppSettings taskAppSettings)
            : base(taskAppSettings.Pagination) { }

        public TaskFilterParameters() { }

        /// <summary>
        /// Filter tasks by Title
        /// </summary>
        public string? Title { get; set; }
    }
}
