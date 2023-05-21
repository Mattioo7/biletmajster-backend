namespace biletmajster_backend.Interfaces;

public class BlobDto
{
    public string? Uri { get; set; }
    public string? Name { get; set; }
    public string? ContentType { get; set; }
    public Stream? Content { get; set; }
}

public class BlobResponseDto
{
    public string? Status { get; set; }
    public bool Error { get; set; }
    public BlobDto Blob { get; set; }

    public BlobResponseDto()
    {
        Blob = new BlobDto();
    }
}

public interface IStorage
{
    public Task<List<BlobDto>> ListFilesAsync(string prefix);

    public Task<BlobResponseDto> UploadFileAsync(IFormFile file, string key);

    public  Task<BlobDto> DownloadFileAsync(string key);
}