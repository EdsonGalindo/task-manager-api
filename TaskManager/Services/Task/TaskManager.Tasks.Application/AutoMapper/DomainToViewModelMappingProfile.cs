using AutoMapper;
using TaskManager.Tasks.Application.ViewModels;
using TaskManager.Tasks.Domain;

namespace TaskManager.Tasks.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<TaskItem, TaskItemViewModel>();
        }
    }
}
