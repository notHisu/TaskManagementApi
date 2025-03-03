using AutoMapper;
using TaskManagementApi.DTOs;
using TaskManagementApi.Models;

namespace TaskManagementApi.Mappers
{
    public class TaskAttachmentMapper : Profile
    {
        public TaskAttachmentMapper() { 
            CreateMap<TaskAttachment, TaskAttachmentResponseDto>();
        }
    }
}
