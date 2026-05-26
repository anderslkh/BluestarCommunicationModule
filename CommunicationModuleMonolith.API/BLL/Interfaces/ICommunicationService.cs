using CommunicationModuleMonolith.API.BLL.Models;

namespace CommunicationModuleMonolith.API.BLL.Interfaces;

public interface ICommunicationService
{
    string GetStatus();
    CommunicationResponse ProcessRequest(CommunicationRequest request);
}
