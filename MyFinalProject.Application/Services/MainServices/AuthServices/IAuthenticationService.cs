using MyFinalProject.Application.Commands;
using MyFinalProject.Application.Results;
using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.MainServices.AuthServices
{
    public interface IAuthenticationService
    {
        Task<RegisterResult> RegisterEmployerAsync(RegisterEmployerCommand command);

        Task<RegisterResult> RegisterJobSeekerAsync(RegisterJobSeekerCommand command);

        Task<LoginResultForRefresh> LoginAsync(LoginUserCommand command);

        Task<string> GenerateTokenAsync(User user);

        Task LogoutAsync(string jti ,DateTime expiresAtUtc);
    }
}