namespace CommunicationModuleMonolith.API.BLL.Models;

public class CommunicationRequest
{
    public string Operation { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
}
