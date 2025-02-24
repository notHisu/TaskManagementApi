using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManagementApi.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }


        [JsonIgnore]
        public ICollection<TaskItem>? Tasks { get; set; } 
    }
}
