using CommunicationModuleMonolith.API.BLL.Handlers;
using CommunicationModuleMonolith.API.BLL.Interfaces;
using CommunicationModuleMonolith.API.BLL.Models;

namespace CommunicationModuleMonolith.API.BLL.Services;

public class CommunicationService : ICommunicationService
{
    private readonly IEnumerable<IOperationHandler> _handlers;

    public CommunicationService(IEnumerable<IOperationHandler> handlers)
    {
        _handlers = handlers;
    }

    public string GetStatus()
    {
        return "Monolith communication service is running";
    }

    public CommunicationResponse ProcessRequest(CommunicationRequest request)
    {
        if (string.IsNullOrEmpty(request.Operation))
        {
            return new CommunicationResponse
            {
                Success = false,
                Message = "Operation is required",
                Result = ""
            };
        }

        var handler = _handlers.FirstOrDefault(h => h.OperationName.Equals(request.Operation, StringComparison.OrdinalIgnoreCase));
        if (handler == null)
        {
            return new CommunicationResponse
            {
                Success = false,
                Message = $"No handler found for operation: {request.Operation}",
                Result = ""
            };
        }

        return handler.Handle(request);
    }
}
