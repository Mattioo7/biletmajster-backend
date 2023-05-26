using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using biletmajster_backend.Interfaces;

namespace biletmajster_backend.Services;

public class AzureStorage : IStorage
{
    private readonly string _connectionString;
    private readonly string _containerName;
    private readonly ILogger<AzureStorage> _logger;

    public AzureStorage(IConfiguration configuration, ILogger<AzureStorage> logger)
    {
        _connectionString = configuration["BlobStorageConnectionString"];
        _containerName = "media";
        _logger = logger;
    }

    public async Task<List<BlobDto>> ListFilesAsync(string prefix)
    {
        BlobContainerClient client = new BlobContainerClient(_connectionString, _containerName);

        List<BlobDto> files = new List<BlobDto>();

        await foreach (BlobItem file in client.GetBlobsAsync(prefix: prefix))
        {
            files.Add(new BlobDto
            {
                Uri = client.Uri.ToString(),
                Name = file.Name,
                ContentType = file.Properties.ContentType
            });
        }

        return files;
    }

    public async Task<BlobResponseDto> UploadFileAsync(IFormFile file, string key)
    {
        BlobResponseDto response = new();

        BlobContainerClient container = new BlobContainerClient(_connectionString, _containerName);

        try
        {
            BlobClient client = container.GetBlobClient(key);

            await using (var data = file.OpenReadStream())
            {
                await client.UploadAsync(data);
            }

            response.Status = $"File {key} uploaded successfully";
            response.Error = false;
            response.Blob.Uri = client.Uri.AbsoluteUri;
            response.Blob.Name = client.Name;
        }
        catch (RequestFailedException ex)
        {
            // Log error to console and create a new response we can return to the requesting method
            _logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
            response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
            response.Error = true;
            return response;
        }

        return response;
    }

    public async Task<BlobDto> DownloadFileAsync(string key)
    {
        BlobContainerClient client = new BlobContainerClient(_connectionString, _containerName);

        try
        {
            BlobClient file = client.GetBlobClient(key);

            if (await file.ExistsAsync())
            {
                var data = await file.OpenReadAsync();
                Stream blobContent = data;

                var content = await file.DownloadContentAsync();

                string name = key;
                string contentType = content.Value.Details.ContentType;

                return new BlobDto { Content = blobContent, Name = name, ContentType = contentType };
            }
        }
        catch (RequestFailedException ex)
            when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
        {
            // Log error to console
            _logger.LogError($"File {key} was not found.");
        }

        // File does not exist, return null and handle that in requesting method
        return null;
    }

    public async Task<bool> DeleteFileAsync(string key)
    {
        BlobContainerClient client = new BlobContainerClient(_connectionString, _containerName);
        try
        {
            BlobClient file = client.GetBlobClient(key);
            await file.DeleteAsync(DeleteSnapshotsOption.IncludeSnapshots);
            return true;
        }
        catch (RequestFailedException ex)
            when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
        {
            // Log error to console
            _logger.LogError($"File {key} was not found.");
        }
        // File does not exist, return null and handle that in requesting method
        return false;
    }
}