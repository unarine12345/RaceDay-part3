using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace RaceDay.API.Services;

public class BlobStorageService(IConfiguration configuration)
{
    public async Task<string> UploadAsync(IFormFile file, string containerName)
    {
        var connectionString = configuration["AzureBlobStorage:ConnectionString"];
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Azure Blob Storage is not configured. Set AzureBlobStorage:ConnectionString.");

        var container = new BlobServiceClient(connectionString).GetBlobContainerClient(containerName);
        await container.CreateIfNotExistsAsync();
        var extension = Path.GetExtension(Path.GetFileName(file.FileName));
        var blob = container.GetBlobClient($"{Guid.NewGuid():N}{extension}");
        await using var stream = file.OpenReadStream();
        await blob.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
        return blob.Uri.ToString();
    }
}
