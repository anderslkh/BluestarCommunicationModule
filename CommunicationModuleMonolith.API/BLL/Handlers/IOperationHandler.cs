using CommunicationModuleMonolith.API.BLL.Models;

namespace CommunicationModuleMonolith.API.BLL.Handlers;

public interface IOperationHandler
{
    string OperationName { get; }
    CommunicationResponse Handle(CommunicationRequest request);
}
