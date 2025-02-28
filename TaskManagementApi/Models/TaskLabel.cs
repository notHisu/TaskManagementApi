using System.Text.Json.Serialization;

namespace TaskManagementApi.Models
{
    public class TaskLabel
    {
        public int TaskId { get; set; }
        public int LabelId { get; set; }

        public TaskItem? Task { get; set; }
        public Label? Label { get; set; }
    }
}
