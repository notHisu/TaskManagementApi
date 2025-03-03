namespace TaskManagementApi.Models
{
    public class TaskAttachment
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public required string FileName { get; set; }
        public required string FileUrl { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public TaskItem? Task { get;set; }
    }
}
