using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models
{
    public class Label
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}
