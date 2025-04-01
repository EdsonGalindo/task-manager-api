using TaskManager.Core.Shared.Task.Filter;

namespace TaskManager.Task.Domain
{
    /// <summary>
    /// The interface for access to the Tasks repository
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <param name="taskFilter">Filter the tasks by optional parameters and limit the results</param>
        /// <returns>A list of one or more Tasks</returns>
        Task<IEnumerable<Task>> GetAllTasksAsync(TaskFilterParameters taskFilter);

        /// <summary>
        /// Get a task by its Id
        /// </summary>
        /// <param name="id">A task Id</param>
        /// <returns>Returns a task</returns>
        Task<IEnumerable<Task>> GetByIdAsync(int id);

        /// <summary>
        /// Check if a task exists by its Id
        /// </summary>
        /// <param name="id">A task Id</param>
        /// <returns>Returns a boolean indicating if a task exists</returns>
        Task<bool> ExistsByIdAsync(int id);

        /// <summary>
        /// Adds a new task
        /// </summary>
        /// <param name="task">A task entity</param>
        /// <returns>Returns a created task Id</returns>
        Task<int> AddAsync(Task task);

        /// <summary>
        /// Updates a task
        /// </summary>
        /// <param name="task">An existent task entity</param>
        /// <returns>Returns a boolean indicating the update success or fail</returns>
        Task<bool> UpdateAsync(Task task);

        /// <summary>
        /// Deletes a task by its Id
        /// </summary>
        /// <param name="id">A task Id</param>
        /// <returns>Returns a boolean indicating the delete success or fail</returns>
        Task<bool> DeleteAsync(int id);
    }
}
