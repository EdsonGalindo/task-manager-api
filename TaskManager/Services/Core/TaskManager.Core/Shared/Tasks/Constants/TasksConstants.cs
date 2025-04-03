namespace TaskManager.Core.Shared.Tasks.Constants
{
    public static class TasksConstants
    {
        public const int TaskTitleMaxLength = 100;
        public const int TaskDescriptionMaxLength = 500;
        public const string TaskItemNotFound = "Tarefa não encontrada";
        public const string TaskItemInvalid = "Tarefa inválida";
        public const string TaskOperationFailed = "Ocorreu uma falha desconhecida, tente novamente ou contate nosso suporte";
        public const string TaskTitleNotInformed = "O Título da tarefa é obrigatório";
        public const string TaskPropertyInvalidLength = "O {0} da tarefa deve conter no máximo {1}";
        public const string TaskStatusIsInvalid = "O Status da tarefa é inválido";

        public static string GetMaxLengthErrorMessage(string fieldName, int maxLength)
        {
            return string.Format(TaskPropertyInvalidLength, fieldName, maxLength);
        }
    }
}
