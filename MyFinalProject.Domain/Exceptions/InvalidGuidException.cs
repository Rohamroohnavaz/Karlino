using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Exceptions
{
    public class InvalidGuidException : Exception
    {
        public InvalidGuidException(string message) : base(message)
        {
            
        }
    }
}
