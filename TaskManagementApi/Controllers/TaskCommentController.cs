using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCommentController : ControllerBase
    {
        private readonly IGenericRepository<TaskComment> _taskCommentRepository;

        public TaskCommentController(IGenericRepository<TaskComment> taskCommentRepository)
        {
            _taskCommentRepository = taskCommentRepository;
        }

        [HttpGet(Name = "GetAllTaskComments")]
        public ActionResult GetAllTaskComments()
        {
            var taskComments = _taskCommentRepository.GetAll();
            return Ok(taskComments);
        }

        [HttpPost(Name = "AddCommentToTask")]
        public ActionResult<TaskComment> AddCommentToTask(TaskComment taskComment)
        {
            if(taskComment == null)
            {
                return BadRequest("TaskComment cannot be null!");
            }

            _taskCommentRepository.Add(taskComment);
            return CreatedAtAction("GetTaskCommentById", new { id = taskComment.Id }, taskComment);
        }

        [HttpDelete("{id}", Name = "DeleteTaskComment")]
        public ActionResult DeleteTaskComment(int id)
        {
            var taskComment = _taskCommentRepository.GetById(id);
            if (taskComment == null)
            {
                return NotFound("TaskComment not found.");
            }

            _taskCommentRepository.Delete(id);
            return NoContent();
        }

    }
}
