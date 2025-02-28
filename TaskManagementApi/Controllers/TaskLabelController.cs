using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.DTOs;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;
using System.Security.Claims;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskLabelController : ControllerBase
    {
        private readonly ITaskLabelRepository<TaskLabel> _taskLabelRepository;
        private readonly ITaskRepository<TaskItem> _taskRepository;
        private readonly IMapper _mapper;

        public TaskLabelController(
            ITaskLabelRepository<TaskLabel> taskLabelRepository,
            ITaskRepository<TaskItem> taskRepository,
            IMapper mapper)
        {
            _taskLabelRepository = taskLabelRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [HttpPost(Name = "AddLabelToTask")]
        public async Task<ActionResult<TaskLabelResponseDto>> AddLabelToTask(TaskLabelCreateDto taskLabelDto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return Unauthorized(new { message = "User not authenticated" });
                }

                var task = await _taskRepository.GetByIdAsync(taskLabelDto.TaskId);
                if (task == null)
                {
                    return NotFound(new { message = "Task not found" });
                }

                if (task.UserId!.ToString() != userId)
                {
                    return Forbid();
                }

                var taskLabelEntity = _mapper.Map<TaskLabel>(taskLabelDto);
                var createdTaskLabel = await _taskLabelRepository.AddAsync(taskLabelEntity);
                var taskLabelResponse = _mapper.Map<TaskLabelResponseDto>(createdTaskLabel);

                return CreatedAtRoute("GetAllTaskLabels", null, taskLabelResponse);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet(Name = "GetAllTaskLabels")]
        public async Task<ActionResult<IEnumerable<TaskLabelResponseDto>>> GetAllTaskLabels()
        {
            try
            {
                var taskLabels = await _taskLabelRepository.GetAllAsync();
                var taskLabelDtos = _mapper.Map<IEnumerable<TaskLabelResponseDto>>(taskLabels);
                return Ok(taskLabelDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{taskId}/{labelId}")]
        public async Task<IActionResult> RemoveLabelFromTask(int taskId, int labelId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return Unauthorized(new { message = "User not authenticated" });
                }
                var task = await _taskRepository.GetByIdAsync(taskId);
                if (task == null)
                {
                    return NotFound(new { message = "Task not found" });
                }
                if (task.UserId!.ToString() != userId)
                {
                    return Forbid();
                }
                var taskLabel = await _taskLabelRepository.GetByIdAsync(taskId, labelId);
                if (taskLabel == null)
                {
                    return NotFound(new { message = "Label not found" });
                }
                await _taskLabelRepository.DeleteAsync(taskId, labelId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
