using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models
{
    public class Label
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<TaskLabel>? TaskLabels { get; set; } = new List<TaskLabel>();
    }
}
