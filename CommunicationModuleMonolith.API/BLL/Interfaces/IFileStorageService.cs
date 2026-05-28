using CommunicationModuleMonolith.API.BLL.Models;

namespace CommunicationModuleMonolith.API.BLL.Interfaces;

public interface IFileStorageService
{
    Task<FileUploadResult> SaveAsync(
        string fileId,
        Stream content,
        CancellationToken cancellationToken);

    Task<FileDownloadResult?> OpenReadAsync(
        string fileId,
        CancellationToken cancellationToken);
}
