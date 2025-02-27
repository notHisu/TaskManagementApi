using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementApi.DTOs;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskCommentController : ControllerBase
    {
        private readonly ITaskCommentRepository<TaskComment> _taskCommentRepository;
        private readonly ITaskRepository<TaskItem> _taskRepository;
        private readonly IMapper _mapper;

        public TaskCommentController(ITaskCommentRepository<TaskComment> taskCommentRepository,
                                     ITaskRepository<TaskItem> taskRepository,
                                     IMapper mapper)
        {
            _taskCommentRepository = taskCommentRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllTaskComments")]
        public async Task<ActionResult<IEnumerable<TaskCommentResponseDto>>> GetAllTaskComments()
        {
            try
            {
                var taskComments = await _taskCommentRepository.GetAllAsync();
                var taskCommentDtos = _mapper.Map<IEnumerable<TaskCommentResponseDto>>(taskComments);
                return Ok(taskCommentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost(Name = "AddCommentToTask")]
        public async Task<ActionResult<TaskCommentResponseDto>> AddCommentToTask(TaskCommentCreateDto taskCommentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                var task = await _taskRepository.GetByIdAsync(taskCommentDto.TaskId);
                if (task == null)
                {
                    return NotFound(new { message = "Task not found" });
                }

                if (task.UserId != userId)
                {
                    return Forbid();
                }

                var taskComment = _mapper.Map<TaskComment>(taskCommentDto);
                var createdComment = await _taskCommentRepository.AddAsync(taskComment);

                var createdCommentDto = _mapper.Map<TaskCommentResponseDto>(createdComment);
                return CreatedAtRoute("GetAllTaskComments", new { id = createdCommentDto.TaskId }, createdCommentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}", Name = "DeleteTaskComment")]
        public async Task<ActionResult> DeleteTaskComment(int id)
        {
            try
            {
                var userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                var comment = await _taskCommentRepository.GetByIdAsync(id);
                if (comment == null)
                {
                    return NotFound(new { message = "Task comment not found" });
                }

                var task = await _taskRepository.GetByIdAsync(comment.TaskId);
                if (task == null)
                {
                    return NotFound(new { message = "Task not found" });
                }

                if (task.UserId != userId)
                {
                    return Forbid(); 
                }

                await _taskCommentRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
