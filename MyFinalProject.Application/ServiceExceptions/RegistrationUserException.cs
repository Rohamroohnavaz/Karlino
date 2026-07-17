using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.ServiceExceptions
{
    public class RegistrationUserException : Exception
    {
        public RegistrationUserException(string message) : base(message)
        {
            
        }
    }
}
