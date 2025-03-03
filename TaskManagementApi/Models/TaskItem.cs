using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManagementApi.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public int? UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User? User { get; set; }
        public Category? Category { get; set; }
        public ICollection<TaskComment>? Comments { get; set; }
        public ICollection<TaskLabel>? TaskLabels { get; set; }
    }
}
