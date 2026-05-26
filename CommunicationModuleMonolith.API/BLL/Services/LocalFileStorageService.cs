using CommunicationModuleMonolith.API.BLL.Interfaces;

namespace CommunicationModuleMonolith.API.BLL.Services;

public class LocalFileStorageService : IFileStorageService
{
    private const int BufferSize = 81920;
    private readonly string _rootPath;

    public LocalFileStorageService(IConfiguration configuration)
    {
        var configuredRootPath = configuration["FileStorage:RootPath"];
        if (string.IsNullOrWhiteSpace(configuredRootPath))
        {
            throw new InvalidOperationException("FileStorage:RootPath is not configured.");
        }

        _rootPath = Path.GetFullPath(configuredRootPath);
    }

    public async Task SaveAsync(string fileId, Stream content, CancellationToken cancellationToken)
    {
        var filePath = GetFilePath(fileId);

        Directory.CreateDirectory(_rootPath);

        await using var fileStream = new FileStream(
            filePath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            BufferSize,
            useAsync: true);

        await content.CopyToAsync(fileStream, cancellationToken);
    }

    public Task<Stream?> OpenReadAsync(string fileId, CancellationToken cancellationToken)
    {
        var filePath = GetFilePath(fileId);

        if (!File.Exists(filePath))
        {
            return Task.FromResult<Stream?>(null);
        }

        Stream stream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            BufferSize,
            useAsync: true);

        return Task.FromResult<Stream?>(stream);
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
