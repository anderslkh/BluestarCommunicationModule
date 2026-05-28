using CommunicationModuleMonolith.API.BLL.Interfaces;
using CommunicationModuleMonolith.API.BLL.Models;

namespace CommunicationModuleMonolith.API.BLL.Services;

public class LocalFileStorageService : IFileStorageService
{
    private const int BufferSize = 81920;
    private readonly string _rootPath;
    private readonly ILogger<LocalFileStorageService> _logger;

    public LocalFileStorageService(IConfiguration configuration, ILogger<LocalFileStorageService> logger)
    {
        _logger = logger;
        var configuredRootPath = configuration["FileStorage:RootPath"];
        if (string.IsNullOrWhiteSpace(configuredRootPath))
        {
            throw new InvalidOperationException("FileStorage:RootPath is not configured.");
        }

        _rootPath = Path.GetFullPath(configuredRootPath);
    }

    public async Task<FileUploadResult> SaveAsync(string fileId, Stream content, CancellationToken cancellationToken)
    {
        var filePath = GetFilePath(fileId);

        _logger.LogInformation("Saving file with id: {FileId} to path: {FilePath}", fileId, filePath);

        Directory.CreateDirectory(_rootPath);

        await using var fileStream = new FileStream(
            filePath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            BufferSize,
            useAsync: true);

        await content.CopyToAsync(fileStream, cancellationToken);

        var BytesWritten = fileStream.Length;

        _logger.LogInformation(
            "File {FileId} saved to local storage with {BytesWritten} bytes",
            fileId,
            BytesWritten);

        return new FileUploadResult
        {
            FileId = fileId,
            BytesWritten = BytesWritten
        };
    }

    public Task<FileDownloadResult?> OpenReadAsync(string fileId, CancellationToken cancellationToken)
    {
        var filePath = GetFilePath(fileId);

        _logger.LogInformation("Opening file with id: {FileId} from path: {FilePath}", fileId, filePath);

        if (!File.Exists(filePath))
        {
            return Task.FromResult<FileDownloadResult?>(null);
        }

        var fileInfo = new FileInfo(filePath);

        Stream stream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            BufferSize,
            useAsync: true);

        _logger.LogInformation(
            "File {FileId} opened from local storage with {ContentLength} bytes",
            fileId,
            fileInfo.Length);

        var result = new FileDownloadResult
        {
            FileId = fileId,
            Content = stream,
            ContentLength = fileInfo.Length,
            ContentType = "application/octet-stream"
        };

        return Task.FromResult<FileDownloadResult?>(result);
    }

    private string GetFilePath(string fileId)
    {
        if (!Guid.TryParse(fileId, out _))
        {
            throw new ArgumentException("Invalid file id.", nameof(fileId));
        }

        var filePath = Path.GetFullPath(Path.Combine(_rootPath, $"{fileId}.bin"));

        if (!filePath.StartsWith(_rootPath, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Resolved file path is outside the storage root.");
        }

        return filePath;
    }
}
