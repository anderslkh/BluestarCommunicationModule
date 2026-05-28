namespace CommunicationModuleMonolith.API.BLL.Models;

public class FileDownloadResult
{
    public string FileId { get; set; } = string.Empty;
    public Stream Content { get; set; } = Stream.Null;
    public long ContentLength { get; set; }
    public string ContentType { get; set; } = "application/octet-stream";
}
