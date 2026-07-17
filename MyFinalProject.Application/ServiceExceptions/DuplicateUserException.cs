using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.ServiceExceptions
{
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException(string key) : base($"We have duplicate user : {key} ,409")
        {
            
        }
    }
}
