using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.ServiceExceptions
{
    public class EnforceApproveException : Exception
    {
        public EnforceApproveException(string message) : base(message)
        {
            
        }
    }
}
