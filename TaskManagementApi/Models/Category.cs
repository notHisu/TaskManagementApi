using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<TaskItem>? Tasks { get; set; } = new List<TaskItem>();
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
