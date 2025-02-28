namespace TaskManagementApi.DTOs
{
    public class TaskLabelCreateDto
    {
        public int TaskId { get; set; }
        public int LabelId { get; set; }
    }

    public class TaskLabelResponseDto
    {
        public int TaskId { get; set; }
        public int LabelId { get; set; }
    }

    public class TaskLabelUpdateDto
    {
        public int? LabelId { get; set; }
    }
}
