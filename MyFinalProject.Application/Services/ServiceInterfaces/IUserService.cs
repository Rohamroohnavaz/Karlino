using MyFinalProject.Application.Commands;
using MyFinalProject.Application.Commands.ViewModels;
using MyFinalProject.Application.DTOs;
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

        Task UpdateProfileUser(UpdateUserProfileDto dto);

        Task ApproveUserAsync(Guid userId);

        Task ApproveUserWithCheckAsync(Guid userId);

        Task<UsersViewModel> GetJobSeekerProfile(Guid userId);
    }
}
