using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.DTOs;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskLabelController : ControllerBase
    {
        private readonly ITaskLabelRepository<TaskLabelResponseDto> _taskLabelRepository;

        public TaskLabelController(ITaskLabelRepository<TaskLabelResponseDto> taskLabelRepository)
        {
            _taskLabelRepository = taskLabelRepository;
        }

        [HttpGet(Name = "GetAllTaskLabels")]
        public ActionResult GetAllTaskLabels()
        {
            try
            {
                var taskLabels = _taskLabelRepository.GetAll();
                return Ok(taskLabels);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        [HttpPost(Name = "AddLabelToTask")]
        public ActionResult<TaskLabelResponseDto> AddLabelToTask(TaskLabelCreateDto taskLabel)
        {
            try
            {
                _taskLabelRepository.Add(taskLabel);
                return CreatedAtRoute("GetAllTaskLabels", taskLabel);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpDelete("{taskId}/{labelId}", Name = "DeleteTaskLabel")]
        public ActionResult DeleteTaskLabel(int taskId, int labelId)
        {
            try
            {
                _taskLabelRepository.Delete(taskId, labelId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

    }
}
