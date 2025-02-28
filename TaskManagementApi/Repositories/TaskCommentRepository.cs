using Microsoft.EntityFrameworkCore;
using TaskManagementApi.DTOs;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories
{
    public class TaskCommentRepository : ITaskCommentRepository<TaskComment>
    {
        private readonly TaskContext _context;

        public TaskCommentRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<TaskComment> AddAsync(TaskComment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "TaskComment cannot be null");
            }

            var taskComment = new TaskComment
            {
                TaskId = entity.TaskId,
                UserId = entity.UserId,
                Content = entity.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _context.TaskComments.AddAsync(taskComment);
            await _context.SaveChangesAsync();

            return taskComment;
        }

        public async Task DeleteAsync(int id)
        {
            var taskComment = await _context.TaskComments.FirstOrDefaultAsync(c => c.Id == id);

            if (taskComment == null)
            {
                throw new KeyNotFoundException("TaskComment not found");
            }

            _context.TaskComments.Remove(taskComment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskComment>> GetAllAsync()
        {
            return await _context.TaskComments.ToListAsync();
        }

        public async Task<TaskComment?> GetByIdAsync(int id)
        {
            return await _context.TaskComments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(int id, TaskComment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "TaskComment cannot be null");
            }

            var taskComment = await _context.TaskComments.FirstOrDefaultAsync(c => c.Id == id);

            if (taskComment == null)
            {
                throw new KeyNotFoundException("TaskComment not found");
            }
            _context.TaskComments.Entry(taskComment).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}
