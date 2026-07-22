using MyFinalProject.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.ServiceInterfaces
{
    public interface IUserService
    {
        Task UpdateUserInfo(UpdateUserInfoCommand command);

        Task AddVipEmployerForAdv(Guid userId);
    }
}
