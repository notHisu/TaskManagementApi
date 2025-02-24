using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public required string Username { get; set; }
        public string? Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        public ICollection<TaskItem>? Tasks { get; set; } = new List<TaskItem>();
    }
}
