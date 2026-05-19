using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationModuleMonolith.Models;

namespace CommunicationModuleMonolith.Handlers
{
    public class GetFileStatusHandler : IOperationHandler
    {
        public string OperationName => "GetFileStatus";

        public CommunicationResponse Handle(CommunicationRequest request)
        {
            // Implement the logic for getting file status here
            return new CommunicationResponse
            {
                Success = true,
                Message = "File status retrieved successfully",
                Result = $"File: {request.Payload}, Status: OK"
            };
        }
    }
}
