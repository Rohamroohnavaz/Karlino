using MyFinalProject.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.AuthServices
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<GenericResult> GenerateToken()
        {
            throw new NotImplementedException();
        }
    }
}
