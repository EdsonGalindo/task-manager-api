using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.Core.Shared.Task.Filter;
using TaskManager.Core.Shared.WebApps.API;
using TaskManager.Tasks.Application.Services;
using TaskManager.Tasks.Application.ViewModels;

namespace TaskManager.WebApp.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/taskmanager")]
    public class TaskManagerController(
        ITasksAppservice tasksAppservice,
        ITaskAppSettings taskAppSettings) : ControllerBase
    {
        private readonly ITasksAppservice _tasksAppService = tasksAppservice;
        private readonly ITaskAppSettings _taskAppSettings = taskAppSettings;

        [HttpPost("tasks")]
        [SwaggerOperation(
            Summary = "Adicionar uma nova Tarefa",
            Description = "Adicionar uma nova Tarefa ao sistema"
        )]
        public async Task<int> AddTaskAsync(TaskItemViewModel task)
        {
            return await _tasksAppService.AddTaskAsync(task);
        }
        
        [HttpPut("tasks")]
        [SwaggerOperation(
            Summary = "Atualizar uma Tarefa",
            Description = "Atualizar uma Tarefa existente no sistema"
        )]
        public async Task<bool> UpdateTaskAsync(TaskItemViewModel task)
        {
            return await _tasksAppService.UpdateTaskAsync(task);
        }
        
        [HttpDelete("tasks/{id:int}")]
        [SwaggerOperation(
            Summary = "Excluir uma Tarefa",
            Description = "Excluir uma Tarefa existente no sistema"
        )]
        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _tasksAppService.DeleteTaskAsync(id);
        }
        
        [HttpPut("tasks/{id:int}/status/pending")]
        [SwaggerOperation(
            Summary = "Definir o Status de uma Tarefa como \"Pendente\"",
            Description = "Definir o Status de uma Tarefa existente no sistema como \"Pendente\""
        )]
        public async Task<bool> SetTaskAsPendingAsync(int id)
        {
            return await _tasksAppService.SetTaskAsPendingAsync(id);
        }
        
        [HttpPut("tasks/{id:int}/status/inprogress")]
        [SwaggerOperation(
            Summary = "Definir o Status de uma Tarefa como \"Em Progresso\"",
            Description = "Definir o Status de uma Tarefa existente no sistema como \"Em Progresso\""
        )]
        public async Task<bool> SetTaskAsInProgressAsync(int id)
        {
            return await _tasksAppService.SetTaskAsInProgressAsync(id);
        }
        
        [HttpPut("tasks/{id:int}/status/completed")]
        [SwaggerOperation(
            Summary = "Definir o Status de uma Tarefa como \"Concluída\"",
            Description = "Definir o Status de uma Tarefa existente no sistema como \"Concluída\""
        )]
        public async Task<bool> SetTaskAsCompletedAsync(int id)
        {
            return await _tasksAppService.SetTaskAsCompletedAsync(id);
        }

        [HttpGet("tasks")]
        [SwaggerOperation(
            Summary = "Obter todas tarefas",
            Description = "Obter todas as Tarefas com filtro por título (opcional) e paginação dos resultados"
        )]
        public async Task<IEnumerable<TaskItemViewModel>> GetAllTasksAsync(
            [FromQuery] TaskFilterParameters taskFilter)
        {
            return await _tasksAppService.GetAllTasksAsync(GetParametrizedFilter(taskFilter));
        }

        [HttpGet("tasks/{id:int}")]
        [SwaggerOperation(
            Summary = "Obter uma tarefa",
            Description = "Obter uma Tarefa através de seu número de identificação"
        )]
        public async Task<TaskItemViewModel?> GetTaskByIdAsync(int id)
        {
            return await _tasksAppService.GetTaskByIdAsync(id);
        }

        [HttpGet("tasks/{id:int}/exists")]
        [SwaggerOperation(
            Summary = "Obter informação se uma tarefa existe",
            Description = "Obter informação se uma tarefa existe através de seu número de identificação"
        )]
        public async Task<bool> GetTaskExistsByIdAsync(int id)
        {
            return await _tasksAppService.GetTaskExistsByIdAsync(id);
        }

        /// <summary>
        /// Get a Task Filter parametrized by the Task App Settings
        /// </summary>
        /// <param name="taskFilter">A Task Filter instance</param>
        /// <returns>A parametrized Task Filter instance</returns>
        private TaskFilterParameters GetParametrizedFilter(TaskFilterParameters taskFilter)
        {
            return new TaskFilterParameters(_taskAppSettings)
            {
                PageNumber = taskFilter.PageNumber,
                PageSize = taskFilter.PageSize,
                Title = taskFilter.Title
            };
        }
    }
}
