using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationModuleMonolith.Models;

namespace CommunicationModuleMonolith.Handlers
{
    public class CreateFileHandler : IOperationHandler
    {
        public string OperationName => "CreateFile";

        public CommunicationResponse Handle(CommunicationRequest request)
        {
            // Implement the logic for creating a file here
            return new CommunicationResponse
            {
                Success = true,
                Message = "File created successfully",
                Result = $"File {request.Payload} created successfully"
            };
        }
    }
}
