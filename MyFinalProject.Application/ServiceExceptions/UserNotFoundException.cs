using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.ServiceExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string input) : base($"{input} not found ! ,404")
        {
            
        }
    }
}
