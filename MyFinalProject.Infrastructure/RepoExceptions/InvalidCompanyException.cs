using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.RepoExceptions
{
    public class InvalidCompanyException : Exception
    {
        public InvalidCompanyException(string message) : base(message)
        {
            
        }
    }
}
