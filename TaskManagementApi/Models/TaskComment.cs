using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models
{
    public class TaskComment
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }

        [Required]
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public User? User { get; set; }
        public TaskItem? Task { get; set; }
    }
}
