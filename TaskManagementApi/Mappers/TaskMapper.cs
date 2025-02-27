using AutoMapper;
using TaskManagementApi.DTOs;
using TaskManagementApi.Models;

namespace TaskManagementApi.Mappers
{
    public class TaskMapper : Profile
    {
        public TaskMapper() {
            CreateMap<TaskItem, TaskResponseDto>();
            CreateMap<TaskCreateDto, TaskItem>();
            CreateMap<TaskUpdateDto, TaskItem>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));    
        }
    }
}
