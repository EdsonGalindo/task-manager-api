using AutoMapper;
using TaskManager.Tasks.Application.ViewModels;
using TaskManager.Tasks.Domain;

namespace TaskManager.Tasks.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<TaskItemViewModel, TaskItem>();
        }
    }
}
