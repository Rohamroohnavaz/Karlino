using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.ServiceExceptions
{
    public class BaseBussinessException : BaseException
    {
        public BaseBussinessException(string message ,string code ,Exception? innerException = null)
            : base(message ,$"ExceptionCode : _{code}" ,innerException)
        {
            
        }
    }
}
