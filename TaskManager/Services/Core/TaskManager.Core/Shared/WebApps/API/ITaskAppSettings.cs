using TaskManager.Core.DomainObjects;

namespace TaskManager.Core.Shared.WebApps.API
{
    public interface ITaskAppSettings
    {
        PaginationSettings Pagination { get; set; }
    }
}
