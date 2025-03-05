using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository<TaskItem> _taskRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public TaskController(ITaskRepository<TaskItem> taskRepository, UserManager<User> userManager, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllTasks")]
        [Authorize]
        public async Task<ActionResult<List<TaskResponseDto>>> GetAllTasks()
        {
            try
            {
                var tasks = await _taskRepository.GetAllAsync();
                var taskResponse = _mapper.Map<List<TaskResponseDto>>(tasks);
                return Ok(taskResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}", Name = "GetTaskById")]
        public async Task<ActionResult<TaskResponseDto>> GetTaskById(int id)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(id);
                if (task == null)
                {
                    return NotFound();
                }

                var taskResponse = _mapper.Map<TaskResponseDto>(task);

                return Ok(taskResponse);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost(Name = "AddTask")]
        [Authorize]
        public async Task<ActionResult<TaskItem>> AddTask(TaskCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return Unauthorized();
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Unauthorized("User not found");
                }

                var task = _mapper.Map<TaskItem>(createDto);
                task.UserId = int.Parse(userId);

                await _taskRepository.AddAsync(task);
                return Ok(task);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}", Name = "UpdateTask")]
        public async Task<ActionResult<TaskItem>> UpdateTask(int id, TaskUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            if (existingTask.UserId != int.Parse(userId))
            {
                return Forbid();
            }

            try
            {
                var task = _mapper.Map(updateDto, existingTask);
                await _taskRepository.UpdateAsync(id, task);
                return Ok(task);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}", Name = "DeleteTask")]
        [Authorize]
        public async Task<ActionResult> DeleteTask(int id)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(id);
                if (task == null)
                {
                    return NotFound();
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null || task.UserId != int.Parse(userId))
                {
                    return Forbid();
                }

                await _taskRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
