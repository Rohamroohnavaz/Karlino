using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.RepoExceptions
{
    public class InvalidRequestResumeException : Exception
    {
        public InvalidRequestResumeException(string message) : base(message)
        {
            
        }
    }
}
