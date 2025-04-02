using TaskManager.Core.Shared.Task.Filter;
using TaskManager.Tasks.Application.ViewModels;

namespace TaskManager.Tasks.Application.Services
{
    /// <summary>
    /// The interface for access to the Tasks App Service
    /// </summary>
    public interface ITasksAppservice
    {
        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <param name="taskFilter">Filter the tasks by optional parameters and limit the results</param>
        /// <returns>A list of one or more Tasks</returns>
        Task<IEnumerable<TaskItemViewModel>> GetAllTasksAsync(TaskFilterParameters taskFilter);

        /// <summary>
        /// Get a task by its Id
        /// </summary>
        /// <param name="id">A task Id</param>
        /// <returns>Returns a task</returns>
        Task<TaskItemViewModel?> GetTaskByIdAsync(int id);

        /// <summary>
        /// Check if a task exists by its Id
        /// </summary>
        /// <param name="id">A task Id</param>
        /// <returns>Returns a boolean indicating if a task exists</returns>
        Task<bool> GetTaskExistsByIdAsync(int id);

        /// <summary>
        /// Adds a new task
        /// </summary>
        /// <param name="task">A task entity</param>
        /// <returns>Returns a created task Id</returns>
        Task<int> AddTaskAsync(TaskItemViewModel task);

        /// <summary>
        /// Updates a task
        /// </summary>
        /// <param name="task">An existent task entity</param>
        /// <returns>Returns a boolean indicating the update success or fail</returns>
        Task<bool> UpdateTaskAsync(TaskItemViewModel task);

        /// <summary>
        /// Deletes a task by its Id
        /// </summary>
        /// <param name="id">A task Id</param>
        /// <returns>Returns a boolean indicating the delete success or fail</returns>
        Task<bool> DeleteTaskAsync(int id);

        /// <summary>
        /// Set a task as pending
        /// </summary>
        /// <param name="id">A task Id</param>
        /// <returns>Returns a boolean indicating success or fail</returns>
        Task<bool> SetTaskAsPendingAsync(int id);

        /// <summary>
        /// Set a task as in progress
        /// </summary>
        /// <param name="id">A task Id</param>
        /// <returns>Returns a boolean indicating success or fail</returns>
        Task<bool> SetTaskAsInProgressAsync(int id);

        /// <summary>
        /// Set a task as completed
        /// </summary>
        /// <param name="id">A task Id</param>
        /// <returns>Returns a boolean indicating success or fail</returns>
        Task<bool> SetTaskAsCompletedAsync(int id);
    }
}
