using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        //private readonly ITaskService _taskRepository;

        private readonly ITaskRepository<TaskResponseDto> _taskRepository;

        public TaskController(ITaskRepository<TaskResponseDto> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet(Name = "GetAllTasks")]
        public ActionResult<List<TaskResponseDto>> GetAllTasks()
        {
            try
            {
                var tasks = _taskRepository.GetAll();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}", Name = "GetTaskById")]
        public ActionResult<TaskResponseDto> GetTaskById(int id)
        {
            try
            {
                var task = _taskRepository.GetById(id);
                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        [HttpPost(Name = "AddTask")]
        public ActionResult<TaskResponseDto> AddTask(TaskCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = new TaskCreateDto
            {
                Title = createDto.Title,
                Description = createDto.Description,
                IsCompleted = createDto.IsCompleted,
                UserId = createDto.UserId,
                CategoryId = createDto.CategoryId
            };

            try
            {
                _taskRepository.Add(task);
                return Ok(task);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}", Name = "UpdateTask")]
        public ActionResult<TaskResponseDto> UpdateTask(int id, TaskUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTask = _taskRepository.GetById(id);
            if (existingTask == null)
            {
                return NotFound();
            }
            try
            {
                _taskRepository.Update(id, updateDto);
                return Ok(updateDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}", Name = "DeleteTask")]
        public ActionResult DeleteTask(int id)
        {
            try
            {
                _taskRepository.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


    }
}

