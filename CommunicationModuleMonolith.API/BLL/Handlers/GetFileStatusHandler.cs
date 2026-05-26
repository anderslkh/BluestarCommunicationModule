using CommunicationModuleMonolith.API.BLL.Models;

namespace CommunicationModuleMonolith.API.BLL.Handlers;

public class GetFileStatusHandler : IOperationHandler
{
    public string OperationName => "GetFileStatus";

    public CommunicationResponse Handle(CommunicationRequest request)
    {
        return new CommunicationResponse
        {
            Success = true,
            Message = "File status retrieved successfully",
            Result = $"File: {request.Payload}, Status: OK"
        };
    }
}
