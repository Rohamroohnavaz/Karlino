using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.ServiceExceptions
{
    public class PermissionDeniedException : BaseBussinessException
    {
        public PermissionDeniedException(Exception? innerException = null)
            : base("Can't Access This Resource", "403", innerException)
        {

        }
    }
}
