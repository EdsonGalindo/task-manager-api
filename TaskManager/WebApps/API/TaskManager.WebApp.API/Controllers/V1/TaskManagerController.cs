using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.Core.Shared.Task.Constants;
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
        public async Task<ActionResult<int>> AddTaskAsync(TaskItemViewModel task)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return CustomResponse(ModelState);
                }

                return CustomResponse(await _tasksAppService.AddTaskAsync(task));
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        
        [HttpPut("tasks")]
        [SwaggerOperation(
            Summary = "Atualizar uma Tarefa",
            Description = "Atualizar uma Tarefa existente no sistema"
        )]
        public async Task<ActionResult<bool>> UpdateTaskAsync(TaskItemViewModel task)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return CustomResponse(ModelState);
                }

                return CustomResponse(await _tasksAppService.UpdateTaskAsync(task));
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        
        [HttpDelete("tasks/{id:int}")]
        [SwaggerOperation(
            Summary = "Excluir uma Tarefa",
            Description = "Excluir uma Tarefa existente no sistema"
        )]
        public async Task<ActionResult<bool>> DeleteTaskAsync(int id)
        {
            try
            {
                return CustomResponse(await _tasksAppService.DeleteTaskAsync(id));
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        
        [HttpPut("tasks/{id:int}/set-pending")]
        [SwaggerOperation(
            Summary = "Definir o Status de uma Tarefa como \"Pendente\"",
            Description = "Definir o Status de uma Tarefa existente no sistema como \"Pendente\""
        )]
        public async Task<ActionResult<bool>> SetTaskAsPendingAsync(int id)
        {
            try
            {
                return CustomResponse(await _tasksAppService.SetTaskAsPendingAsync(id));
            }
            catch(Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        
        [HttpPut("tasks/{id:int}/start")]
        [SwaggerOperation(
            Summary = "Definir o Status de uma Tarefa como \"Em Progresso\"",
            Description = "Definir o Status de uma Tarefa existente no sistema como \"Em Progresso\""
        )]
        public async Task<ActionResult<bool>> SetTaskAsInProgressAsync(int id)
        {
            try
            {
                return CustomResponse(await _tasksAppService.SetTaskAsInProgressAsync(id));
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        
        [HttpPut("tasks/{id:int}/complete")]
        [SwaggerOperation(
            Summary = "Definir o Status de uma Tarefa como \"Concluída\"",
            Description = "Definir o Status de uma Tarefa existente no sistema como \"Concluída\""
        )]
        public async Task<ActionResult<bool>> SetTaskAsCompletedAsync(int id)
        {
            try
            {
                return CustomResponse(await _tasksAppService.SetTaskAsCompletedAsync(id));
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

        [HttpGet("tasks")]
        [SwaggerOperation(
            Summary = "Obter todas tarefas",
            Description = "Obter todas as Tarefas com filtro por título (opcional) e paginação dos resultados"
        )]
        public async Task<ActionResult<IEnumerable<TaskItemViewModel>>> GetAllTasksAsync(
            [FromQuery] TaskFilterParameters taskFilter)
        {
            try
            {
                var response = await _tasksAppService.GetAllTasksAsync(GetParametrizedFilter(taskFilter));
                return CustomResponse(response);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

        [HttpGet("tasks/{id:int}")]
        [SwaggerOperation(
            Summary = "Obter uma tarefa",
            Description = "Obter uma Tarefa através de seu número de identificação"
        )]
        public async Task<ActionResult<TaskItemViewModel>> GetTaskByIdAsync(int id)
        {
            try
            {
                return CustomResponse(await _tasksAppService.GetTaskByIdAsync(id));
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

        [HttpGet("tasks/{id:int}/exists")]
        [SwaggerOperation(
            Summary = "Obter informação se uma tarefa existe",
            Description = "Obter informação se uma tarefa existe através de seu número de identificação"
        )]
        public async Task<ActionResult<bool>> GetTaskExistsByIdAsync(int id)
        {
            try
            {
                return CustomResponse(await _tasksAppService.GetTaskExistsByIdAsync(id));
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
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
                DueDate = taskFilter.DueDate,
                Status = taskFilter.Status,
                Title = taskFilter.Title
            };
        }

        /// <summary>
        /// Returns a custom ActionResult to the requester
        /// </summary>
        /// <param name="response">A response obejct</param>
        /// <returns>Returns a custom ActionResult Status Code 
        /// according with the "response" parameter data</returns>
        private ActionResult CustomResponse(object? response)
        {
            if (response == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = TasksConstants.TaskOperationFailed
                });
            }

            if (response is ModelStateDictionary modelStateDictionary)
            {
                var errors = modelStateDictionary.Values.SelectMany(v => v.Errors);

                return BadRequest(new
                {
                    success = false,
                    message = errors.Select(error => 
                        error.Exception == null ? 
                        error.ErrorMessage : error.Exception.Message)
                });
            }

            if (response is Exception exception)
            {
                return BadRequest(new 
                {
                    success = false,
                    message = exception.Message
                });
            }

            return Ok(new
            {
                success = true,
                data = response
            });
        }
    }
}
