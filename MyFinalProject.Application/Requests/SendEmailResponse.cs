using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Requests
{
    public class SendEmailResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
        public DateTime SendDate { get; set; } = DateTime.UtcNow;
    }
}
