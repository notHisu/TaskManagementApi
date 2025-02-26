using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models
{
    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class TaskCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int UserId { get; set; }
    }

    public class TaskUpdateDto
    {
        [StringLength(100, MinimumLength = 1)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public bool? IsCompleted { get; set; }

        public int? CategoryId { get; set; }
    }
}