namespace TaskManager.Core.Shared.Task.Domain
{
    /// <summary>
    /// Provides the options of Status Enums for a task
    /// </summary>
    public class TaskStatus
    {
        /// <summary>
        /// Tasks status options
        /// </summary>
        public enum StatusEnum
        {
            /// <summary>
            /// The task is pending
            /// </summary>
            Pending = 1,

            /// <summary>
            /// The task is in progress
            /// </summary>
            InProgress = 2,

            /// <summary>
            /// The task is completed
            /// </summary>
            Completed = 3
        }
    }
}
