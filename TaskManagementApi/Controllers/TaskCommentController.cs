using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.DTOs;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCommentController : ControllerBase
    {
        private readonly ITaskCommentRepository<TaskCommentResponseDto> _taskCommentRepository;

        public TaskCommentController(ITaskCommentRepository<TaskCommentResponseDto> taskCommentRepository)
        {
            _taskCommentRepository = taskCommentRepository;
        }

        [HttpGet(Name = "GetAllTaskComments")]
        public ActionResult GetAllTaskComments()
        {
            try
            {
                var taskComments = _taskCommentRepository.GetAll();
                return Ok(taskComments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost(Name = "AddCommentToTask")]
        public ActionResult<TaskCommentResponseDto> AddCommentToTask(TaskCommentCreateDto taskComment)
        {
            try
            {
                _taskCommentRepository.Add(taskComment);
                return CreatedAtRoute("GetAllTaskComments", new { }, taskComment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}", Name = "DeleteTaskComment")]
        public ActionResult DeleteTaskComment(int id)
        {
            try
            {
                _taskCommentRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }   
        }

    }
}
