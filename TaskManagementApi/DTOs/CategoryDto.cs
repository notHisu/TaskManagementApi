namespace TaskManagementApi.DTOs
{
    public class CategoryCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class CategoryResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }

    public class CategoryUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

}
