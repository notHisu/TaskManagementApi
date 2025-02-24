using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskLabelController : ControllerBase
    {
        private readonly IGenericRepository<TaskLabel> _taskLabelRepository;

        public TaskLabelController(IGenericRepository<TaskLabel> taskLabelRepository)
        {
            _taskLabelRepository = taskLabelRepository;
        }

        [HttpGet(Name = "GetAllTaskLabels")]
        public ActionResult GetAllTaskLabels()
        {
            var taskLabels = _taskLabelRepository.GetAll();
            return Ok(taskLabels);
        }

        [HttpPost(Name = "AddLabelToTask")]
        public ActionResult<TaskLabel> AddLabelToTask(TaskLabel taskLabel)
        {
            _taskLabelRepository.Add(taskLabel);
            return CreatedAtAction("GetTaskLabelById", new { taskId = taskLabel.TaskId, labelId = taskLabel.LabelId }, taskLabel);
        }

    }
}
