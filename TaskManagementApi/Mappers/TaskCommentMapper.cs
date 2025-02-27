using AutoMapper;
using TaskManagementApi.DTOs;
using TaskManagementApi.Models;

namespace TaskManagementApi.Mappers
{
    public class TaskCommentMapper : Profile
    {
        public TaskCommentMapper()
        {
            CreateMap<TaskComment, TaskCommentResponseDto>();
            CreateMap<TaskCommentCreateDto, TaskComment>();
            CreateMap<TaskCommentUpdateDto, TaskComment>();
        }
    }
}
