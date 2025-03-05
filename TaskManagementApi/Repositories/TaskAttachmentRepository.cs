using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories
{
    public class TaskAttachmentRepository : ITaskAttachmentRepository
    {
        private readonly TaskContext _context;

        public TaskAttachmentRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<int> AddAttachmentAsync(TaskAttachment attachment)
        {
            await _context.TaskAttachments.AddAsync(attachment);
            await _context.SaveChangesAsync();
            return attachment.Id;
        }

        public async Task DeleteAttachmentAsync(int attachmentId)
        {
            var attachment = await _context.TaskAttachments.FindAsync(attachmentId);
            if (attachment != null)
            {
                _context.TaskAttachments.Remove(attachment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TaskAttachment>> GetAllAttachmentsByTaskIdAsync(int taskId)
        {
            return await _context.TaskAttachments.Where(x => x.TaskId == taskId).ToListAsync();
        }
    }
}
