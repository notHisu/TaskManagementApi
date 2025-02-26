using TaskManagementApi.DTOs;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories
{
    public class TaskCommentRepository : ITaskCommentRepository<TaskCommentResponseDto>
    {
        private readonly TaskContext _context;

        public TaskCommentRepository(TaskContext context)
        {
            _context = context;
        }

        private static TaskCommentResponseDto ToDto(TaskComment taskComment)
        {
            if (taskComment == null)
            {
                throw new ArgumentNullException(nameof(taskComment));
            }
            return new TaskCommentResponseDto
            {
                TaskId = taskComment.TaskId,
                UserId = taskComment.UserId,
                Content = taskComment.Content,
                CreatedAt = taskComment.CreatedAt
            };
        }

        private static IEnumerable<TaskCommentResponseDto> ToDtos(IEnumerable<TaskComment> taskComments)
        {
            return taskComments.Select(ToDto).ToList();
        }

        public void Add(TaskCommentCreateDto entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("TaskComment cannot be null");
            }

            var taskComment = new TaskComment
            {
                TaskId = entity.TaskId,
                UserId = entity.UserId,
                Content = entity.Content
            };

            _context.TaskComments.Add(taskComment);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var taskComment = _context.TaskComments.FirstOrDefault(c => c.Id == id);

            if(taskComment != null) {
                _context.TaskComments.Remove(taskComment);
                _context.SaveChanges();
            }
        }

        public IEnumerable<TaskCommentResponseDto> GetAll()
        {
            var taskComments = _context.TaskComments.ToList();
            return ToDtos(taskComments);
        }

        public TaskCommentResponseDto? GetById(int id)
        {
            var taskComment = _context.TaskComments.FirstOrDefault(c => c.Id == id);
            return taskComment != null ? ToDto(taskComment) : null;
        }

        public void Update(int id, TaskCommentUpdateDto entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("TaskComment cannot be null");
            }

            var taskComment = _context.TaskComments.FirstOrDefault(c => c.Id == id);

            if(taskComment != null) {
               throw new InvalidOperationException("TaskComment not found");
            }

            if(entity.Content != null)
            {
                taskComment!.Content = entity.Content;
            }

            _context.TaskComments.Update(taskComment!);
            _context.SaveChanges();
        }
    }
}
