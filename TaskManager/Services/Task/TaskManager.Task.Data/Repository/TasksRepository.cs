using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Shared.Task.Filter;
using TaskManager.Tasks.Domain;

namespace TaskManager.Tasks.Data.Repository
{
    /// <summary>
    /// The repository for access to the Tasks database
    /// </summary>
    /// <param name="taskContext">The Tasks Db Context</param>
    public class TasksRepository(TasksContext taskContext) : ITasksRepository
    {
        private readonly TasksContext _taskContext = taskContext;

        /// <inheritdoc/>
        public async Task<int> AddAsync(TaskItem task)
        {
            await _taskContext.Tasks.AddAsync(task);
            await SaveChangesAsync();

            return task.Id;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _taskContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return false;
            }

            _taskContext.Tasks.Remove(task);

            return await SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _taskContext.Tasks.AnyAsync(t => t.Id == id);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync(TaskFilterParameters taskFilter)
        {
            var query = _taskContext.Tasks
                .Where(t => string.IsNullOrEmpty(taskFilter.Title) || 
                       t.Title.Contains(taskFilter.Title))
                .AsQueryable();

            return await ToPagedList(query, taskFilter.PageNumber, taskFilter.PageSize);
        }

        /// <inheritdoc />
        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _taskContext.Tasks.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(TaskItem task)
        {
            _taskContext.Tasks.Update(task);
            return await SaveChangesAsync();
        }

        /// <summary>
        /// Save changes in the database
        /// </summary>
        /// <returns>A boolean indicating success or fail for an operation</returns>
        private async Task<bool> SaveChangesAsync()
        {
            return await _taskContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Converts a query to a paged list
        /// </summary>
        /// <param name="sourceQuery">A query to return its result paged</param>
        /// <param name="pageNumber">The desired page number</param>
        /// <param name="pageSize">The amount of registers to be retuned on each page</param>
        /// <returns>Returns a query result paged</returns>
        private async static Task<IEnumerable<TaskItem>> ToPagedList(
            IQueryable<TaskItem> sourceQuery,
            int pageNumber,
            int pageSize)
        {
            return await sourceQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
