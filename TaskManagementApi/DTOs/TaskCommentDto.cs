namespace TaskManagementApi.DTOs
{
    public class TaskCommentCreateDto
    {
        public string Content { get; set; } = string.Empty;
        public int TaskId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class TaskCommentResponseDto
    {
        public string Content { get; set; } = string.Empty;
        public int TaskId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class TaskCommentUpdateDto
    {
        public string? Content { get; set; }
    }
}
