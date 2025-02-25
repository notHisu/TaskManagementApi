using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManagementApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public required string Username { get; set; }
        public string? Email { get; set; }

        [Required]
        public required string PasswordSalt { get; set; }  

        [Required]
        public required string PasswordHash { get; set; }

        [JsonIgnore]
        public ICollection<TaskItem>? Tasks { get; set; }
    }
}
