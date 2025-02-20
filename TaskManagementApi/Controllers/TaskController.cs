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
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet(Name= "GetAllTasks")]
        public ActionResult<List<TaskItem>> GetAllTasks()
        {
            var tasks = _taskService.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}", Name = "GetTaskById")]
        public ActionResult<TaskItem> GetTaskById(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost(Name = "AddTask")]
        public ActionResult<TaskItem> AddTask(TaskItem task)
        {
            _taskService.AddTask(task);
            return CreatedAtRoute("GetTaskById", new { id = task.Id }, task);
        }

        [HttpPut("{id}", Name = "UpdateTask")]
        public ActionResult<TaskItem> UpdateTask(int id, TaskItem task)
        {
            var existingTask = _taskService.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound();
            }
            task.Id = id;
            _taskService.UpdateTask(task);
            return Ok(task);
        }

        [HttpDelete("{id}", Name = "DeleteTask")]
        public ActionResult DeleteTask(int id)
        {
            var existingTask = _taskService.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound();
            }
            _taskService.DeleteTask(id);
            return NoContent();
        }
    }
}
