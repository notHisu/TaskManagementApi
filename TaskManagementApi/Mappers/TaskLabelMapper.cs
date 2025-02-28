using AutoMapper;
using TaskManagementApi.DTOs;
using TaskManagementApi.Models;

namespace TaskManagementApi.Mappers
{
    public class TaskLabelMapper : Profile
    {
        public TaskLabelMapper() {
            CreateMap<TaskLabel, TaskLabelResponseDto>();
            CreateMap<TaskLabelCreateDto, TaskLabel>();
            CreateMap<TaskLabelUpdateDto, TaskLabel>();
        }
    }
}
