using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.ServiceExceptions
{
    public class DuplicateUserException : BaseBussinessException
    {
        public DuplicateUserException(string key,Exception? innerException = null) :
            base($"We have duplicate user : {key}" ,"409",innerException)
        {
            
        }
    }
}
