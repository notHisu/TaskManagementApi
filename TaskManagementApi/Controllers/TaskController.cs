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

        private readonly IGenericRepository<TaskItem> _taskRepository;

        public TaskController(IGenericRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet(Name= "GetAllTasks")]
        public ActionResult<List<TaskItem>> GetAllTasks()
        {
            var tasks = _taskRepository.GetAll();
            return Ok(tasks);
        }

        [HttpGet("{id}", Name = "GetTaskById")]
        public ActionResult<TaskItem> GetTaskById(int id)
        {
            var task = _taskRepository.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost(Name = "AddTask")]
        public ActionResult<TaskItem> AddTask(TaskItem task)
        {
            _taskRepository.Add(task);
            return CreatedAtAction("GetTaskById", new { id = task.Id }, task);
        }

        [HttpPut("{id}", Name = "UpdateTask")]
        public ActionResult<TaskItem> UpdateTask(int id, TaskItem task)
        {
            var existingTask = _taskRepository.GetById(id);
            if (existingTask == null)
            {
                return NotFound();
            }
            task.Id = id;
            _taskRepository.Update(task);
            return Ok(task);
        }

        [HttpDelete("{id}", Name = "DeleteTask")]
        public ActionResult DeleteTask(int id)
        {
            var existingTask = _taskRepository.GetById(id);
            if (existingTask == null)
            {
                return NotFound();
            }
            _taskRepository.Delete(id);
            return NoContent();
        }
    }
}
