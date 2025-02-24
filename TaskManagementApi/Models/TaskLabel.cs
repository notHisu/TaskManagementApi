using System.Text.Json.Serialization;

namespace TaskManagementApi.Models
{
    public class TaskLabel
    {
        public int TaskId { get; set; }
        public int LabelId { get; set; }

        [JsonIgnore]
        public TaskItem? Task { get; set; }

        [JsonIgnore]
        public Label? Label { get; set; }
    }
}
