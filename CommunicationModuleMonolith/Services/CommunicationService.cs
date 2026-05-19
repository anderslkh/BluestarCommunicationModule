using CommunicationModuleMonolith.Interfaces;
using CommunicationModuleMonolith.Models;
using CommunicationModuleMonolith.Handlers;

namespace CommunicationModuleMonolith.Services;

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
        // Simulate processing the request

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
