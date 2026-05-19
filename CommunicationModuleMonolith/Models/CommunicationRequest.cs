using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationModuleMonolith.Models
{
    public class CommunicationRequest
    {
        public string Operation { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;

    }
}
