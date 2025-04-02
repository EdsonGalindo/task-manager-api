using System.ComponentModel.DataAnnotations;
using static TaskManager.Core.Shared.Task.Domain.TaskStatus;

namespace TaskManager.Tasks.Application.ViewModels
{
    public class TaskItemViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Título é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Título deve ter no mínimo {2} e no máximo {1} caracteres", MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "O campo Descrição deve ter no máximo {1} caracteres")]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        [Range(1, 3, ErrorMessage = "O Status deve ser um número entre {1} e {2}")]
        public StatusEnum? Status { get; set; } = StatusEnum.Pending;
    }
}
