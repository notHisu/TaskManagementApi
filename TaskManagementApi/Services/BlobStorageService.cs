using Azure.Storage.Blobs;
using TaskManagementApi.Interfaces;

namespace TaskManagementApi.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobContainerClient _blobContainerClient;

        public BlobStorageService(IConfiguration configuration) {
            var connectionString = configuration["StorageConnection:ConnectionString"]!;
            var containerName = configuration["StorageConnection:ContainerName"]!;

            _blobContainerClient = new BlobContainerClient(connectionString, containerName);
            _blobContainerClient.CreateIfNotExists();
        }
        public async Task DeleteFileAsync(string fileName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is not valid");
            }

            var blobClient = _blobContainerClient.GetBlobClient(file.FileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.AbsoluteUri;
        }
    }
}
