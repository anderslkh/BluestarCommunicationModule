namespace CommunicationModuleMonolith.API.BLL.Interfaces;

public interface IFileStorageService
{
    Task SaveAsync(
        string fileId,
        Stream content,
        CancellationToken cancellationToken);

    Task<Stream?> OpenReadAsync(
        string fileId,
        CancellationToken cancellationToken);
}
