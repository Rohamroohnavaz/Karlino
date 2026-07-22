using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.ServiceExceptions
{
    public class ItemNotFoundException : BaseBussinessException
    {
        public ItemNotFoundException(string itemName, Type type, Exception? subsetException = null)
            : base($"{itemName} Not Found !", $"{type.FullName}_404", subsetException)
        {

        }
    }
}
