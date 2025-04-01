using TaskManager.Core.DomainObjects;

namespace TaskManager.Core.Shared.WebApps.API
{
    public class TaskAppSettings : ITaskAppSettings
    {
        public required PaginationSettings Pagination { get; set; }
    }
}
