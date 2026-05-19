using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationModuleMonolith.Models;

namespace CommunicationModuleMonolith.Handlers
{
    public interface IOperationHandler
    {
        string OperationName { get; }
        CommunicationResponse Handle(CommunicationRequest request);
    }
}
