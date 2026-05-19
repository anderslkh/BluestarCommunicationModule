using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationModuleMonolith.Models
{
    public class CommunicationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
    }
}
