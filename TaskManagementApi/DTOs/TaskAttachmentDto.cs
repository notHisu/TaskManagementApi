using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.DTOs
{
    public class TaskAttachmentResponseDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public int TaskId { get; set; }
    }
}
