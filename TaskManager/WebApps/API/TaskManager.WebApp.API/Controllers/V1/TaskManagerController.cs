using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
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
        ITaskAppSettings taskAppSettings,
        ILogger<TaskManagerController> logger) : ControllerBase
    {
        private readonly ITasksAppservice _tasksAppService = tasksAppservice;
        private readonly ITaskAppSettings _taskAppSettings = taskAppSettings;
        private readonly ILogger<TaskManagerController> _logger = logger;

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
                    LogModelValidationFail(task, ModelState);
                    return CustomResponse(ModelState);
                }

                return CustomResponse(await _tasksAppService.AddTaskAsync(task));
            }
            catch (Exception exception)
            {
                LogExceptions(exception, task);
                return CustomResponse(exception);
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
                    LogModelValidationFail(task, ModelState);
                    return CustomResponse(ModelState);
                }

                return CustomResponse(await _tasksAppService.UpdateTaskAsync(task));
            }
            catch (Exception exception)
            {
                LogExceptions(exception, task);
                return CustomResponse(exception);
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
            catch (Exception exception)
            {
                LogExceptions(exception);
                return CustomResponse(exception);
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
            catch(Exception exception)
            {
                LogExceptions(exception);
                return CustomResponse(exception);
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
            catch (Exception exception)
            {
                LogExceptions(exception);
                return CustomResponse(exception);
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
            catch (Exception exception)
            {
                LogExceptions(exception);
                return CustomResponse(exception);
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
                if (ModelState.IsValid == false)
                {
                    LogModelValidationFail(taskFilter, ModelState);
                    return CustomResponse(ModelState);
                }

                var response = await _tasksAppService.GetAllTasksAsync(GetParametrizedFilter(taskFilter));
                return CustomResponse(response);
            }
            catch (Exception exception)
            {
                LogExceptions(exception);
                return CustomResponse(exception);
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
            catch (Exception exception)
            {
                LogExceptions(exception);
                return CustomResponse(exception);
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
            catch (Exception exception)
            {
                LogExceptions(exception);
                return CustomResponse(exception);
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
            response ??= new object();

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

        /// <summary>
        /// Log a Model validation fail with error messages and the View Model
        /// </summary>
        /// <param name="methodName">The name of the method where occurred the validation fail</param>
        /// <param name="objectToLog">The validated View Model</param>
        /// <param name="modelState">The Model State instance</param>
        private void LogModelValidationFail(
            object objectToLog,
            ModelStateDictionary modelState,
            [CallerMemberName] string methodName = "")
        {
            _logger.LogWarning(
                "{methodName} - Model State Invalid" +
                " - Messages: {modelStateMessages}" +
                " - Model: {taskItem}",
                methodName,
                GetModelStateErrorMessagesAsString(modelState),
                JsonSerializer.Serialize(objectToLog));
        }

        /// <summary>
        /// Log a Model validation fail with error messages and the View Model
        /// </summary>
        /// <param name="methodName">The name of the method where occurred the validation fail</param>
        /// <param name="objectToLog">The validated View Model</param>
        /// <param name="modelState">The Model State instance</param>
        private void LogExceptions(
            Exception exception,
            object? objectToLog = null,
            [CallerMemberName] string methodName = "")
        {
            _logger.LogWarning(
                "{methodName} - Model State Invalid" +
                " - Message: {modelStateMessages}" +
                " - Model: {taskItem}",
                methodName,
                exception.Message,
                objectToLog != null ? JsonSerializer.Serialize(objectToLog) : string.Empty);
        }

        /// <summary>
        /// Get the Model State validation messages
        /// </summary>
        /// <param name="modelStateDictionary">A Model State</param>
        /// <returns>Returns the Model State validation messages as an unified string</returns>
        private static string GetModelStateErrorMessagesAsString(ModelStateDictionary modelStateDictionary)
        {
            var errors = modelStateDictionary.Values.SelectMany(v => v.Errors);

            var errorsMessages = errors.Select(error =>
                    error.Exception == null ?
                    error.ErrorMessage : error.Exception.Message);

            return string.Join("; ", errorsMessages);
        }
    }
}
