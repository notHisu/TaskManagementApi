namespace TaskManagementApi.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task DeleteFileAsync(string fileName);
    }
}
