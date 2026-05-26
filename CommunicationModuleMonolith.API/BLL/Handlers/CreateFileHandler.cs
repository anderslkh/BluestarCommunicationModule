using CommunicationModuleMonolith.API.BLL.Models;

namespace CommunicationModuleMonolith.API.BLL.Handlers;

public class CreateFileHandler : IOperationHandler
{
    public string OperationName => "CreateFile";

    public CommunicationResponse Handle(CommunicationRequest request)
    {
        return new CommunicationResponse
        {
            Success = true,
            Message = "File created successfully",
            Result = $"File {request.Payload} created successfully"
        };
    }
}
