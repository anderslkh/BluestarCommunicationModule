using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationModuleMonolith.Models;

namespace CommunicationModuleMonolith.Interfaces
{
    public interface ICommunicationService
    {
        string GetStatus();
        CommunicationResponse ProcessRequest(CommunicationRequest request);
    }
}
