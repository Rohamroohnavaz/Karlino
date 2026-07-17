using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Results
{
    public class RegisterResult
    {
        public RegisterResult(Guid id)
        {
            ResultId = id;   
        }
        public Guid ResultId;
    }
}
