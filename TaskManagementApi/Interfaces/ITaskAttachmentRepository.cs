using TaskManagementApi.Models;

namespace TaskManagementApi.Interfaces
{
    public interface ITaskAttachmentRepository
    {
        Task<int> AddAttachmentAsync(TaskAttachment attachment);
        Task DeleteAttachmentAsync(int attachmentId);
        Task<List<TaskAttachment>> GetAllAttachmentsByTaskIdAsync(int taskId);
    }
}
