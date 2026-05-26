namespace CommunicationModuleMonolith.API.BLL.Models;

public class CommunicationResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
}
