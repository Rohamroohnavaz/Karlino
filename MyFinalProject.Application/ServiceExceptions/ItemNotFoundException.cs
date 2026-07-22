using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.ServiceExceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string itemName ,Type type) : base("")
        {
            
        }
    }
}
