namespace CommunicationModuleMonolith.API.BLL.Models;

public class FileUploadResult
{
    public string FileId { get; set; } = string.Empty;
    public long BytesWritten { get; set; }
}
