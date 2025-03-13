using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.DTOs;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskAttachmentController : ControllerBase
    {
        private readonly ITaskAttachmentRepository _taskAttachmentRepository;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TaskItem> _taskRepository;

        public TaskAttachmentController(ITaskAttachmentRepository taskAttachmentRepository, IBlobStorageService blobStorageService, IMapper mapper, IGenericRepository<TaskItem> taskRepository)
        {
            _taskAttachmentRepository = taskAttachmentRepository;
            _blobStorageService = blobStorageService;
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        [HttpPost(Name = "Upload an attachment")]
        public async Task<IActionResult> UploadAnAttachment(IFormFile file, [FromForm] int taskId)
        {
            if (file == null)
            {
                return BadRequest("File is not valid");
            }

            if (_taskRepository.GetByIdAsync(taskId) == null)
            {
                return BadRequest("Task not found");
            }

            var fileName = await _blobStorageService.UploadFileAsync(file);

            var attachment = new TaskAttachment
            {
                TaskId = taskId,
                FileName = fileName,
                FileUrl = $"{ fileName }"
            };
            await _taskAttachmentRepository.AddAttachmentAsync(attachment);

            var attachmentResponse = _mapper.Map<TaskAttachmentResponseDto>(attachment);
            return Ok(attachmentResponse);
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetAttachmentsByTaskId(int taskId)
        {
            var attachments = await _taskAttachmentRepository.GetAllAttachmentsByTaskIdAsync(taskId);
            var attachmentResponse = _mapper.Map<List<TaskAttachmentResponseDto>>(attachments);
            return Ok(attachmentResponse);
        }

        [HttpDelete("{attachmentId}")]
        public async Task<IActionResult> DeleteAttachment(int attachmentId)
        {
            await _taskAttachmentRepository.DeleteAttachmentAsync(attachmentId);
            return Ok();
        }

    }
}
