using AutoMapper;
using System.Threading.Tasks;
using TaskManager.Core.Shared.Task.Constants;
using TaskManager.Core.Shared.Task.Filter;
using TaskManager.Tasks.Application.ViewModels;
using TaskManager.Tasks.Domain;

namespace TaskManager.Tasks.Application.Services
{
    /// <summary>
    /// The Tasks App Service
    /// </summary>
    /// <param name="tasksRepository">A Tasks Repository instance received by Dependency Injection</param>
    /// <param name="mapper">An AutoMapper instance received by Dependency Injection</param>
    public class TasksAppService(
        ITasksRepository tasksRepository,
        IMapper mapper) : ITasksAppservice
    {
        private readonly ITasksRepository _tasksRepository = tasksRepository;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc/>
        public async Task<int> AddTaskAsync(TaskItemViewModel task)
        {
            return await _tasksRepository.AddAsync(_mapper.Map<TaskItem>(task));
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _tasksRepository.DeleteAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TaskItemViewModel>> GetAllTasksAsync(
            TaskFilterParameters taskFilter)
        {
            return _mapper.Map<IEnumerable<TaskItemViewModel>>(
                await _tasksRepository.GetAllTasksAsync(taskFilter));
        }

        /// <inheritdoc/>
        public async Task<TaskItemViewModel?> GetTaskByIdAsync(int id)
        {
            ValidateTaskId(id);
            return _mapper.Map<TaskItemViewModel?>(await _tasksRepository.GetByIdAsync(id));
        }

        /// <inheritdoc/>
        public async Task<bool> GetTaskExistsByIdAsync(int id)
        {
            ValidateTaskId(id);
            return await _tasksRepository.ExistsByIdAsync(id);
        }

        /// <inheritdoc/>
        public async Task<bool> SetTaskAsCompletedAsync(int id)
        {
            ValidateTaskId(id);

            var taskItem = await _tasksRepository.GetByIdAsync(id) ?? 
                                 throw new Exception(TasksConstants.TaskItemNotFound);
            ValidateTaskExists(taskItem);

            taskItem.MarkAsCompleted();

            return await _tasksRepository.UpdateAsync(taskItem);
        }

        /// <inheritdoc/>
        public async Task<bool> SetTaskAsInProgressAsync(int id)
        {
            ValidateTaskId(id);

            var taskItem = await _tasksRepository.GetByIdAsync(id) ??
                                 throw new Exception(TasksConstants.TaskItemNotFound);
            ValidateTaskExists(taskItem);

            taskItem.MarkAsInProgress();

            return await _tasksRepository.UpdateAsync(taskItem);
        }

        /// <inheritdoc/>
        public async Task<bool> SetTaskAsPendingAsync(int id)
        {
            ValidateTaskId(id);

            var taskItem = await _tasksRepository.GetByIdAsync(id) ??
                                 throw new Exception(TasksConstants.TaskItemNotFound);
            ValidateTaskExists(taskItem);

            taskItem.MarkAsPending();

            return await _tasksRepository.UpdateAsync(taskItem);
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateTaskAsync(TaskItemViewModel task)
        {
            ValidateTaskId(task.Id);

            return await _tasksRepository.UpdateAsync(_mapper.Map<TaskItem>(task));
        }

        private static void ValidateTaskId(int id)
        {
            if (id == 0)
            {
                throw new Exception(TasksConstants.TaskItemInvalid);
            }
        }

        private static void ValidateTaskExists(TaskItem taskItem)
        {
            if (taskItem == null)
            {
                throw new Exception(TasksConstants.TaskItemNotFound);
            }
        }
    }
}
