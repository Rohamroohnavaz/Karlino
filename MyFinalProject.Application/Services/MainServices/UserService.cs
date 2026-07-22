using Microsoft.AspNetCore.Identity;
using MyFinalProject.Application.Commands;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.ServiceExceptions;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.MainServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task AddVipEmployerForAdv(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
                throw new UserNotFoundException($"{nameof(user)} not found !!");

            await _userManager.AddClaimAsync(user, ClaimConstants.VipEmployer);
        }

        public async Task UpdateUserInfo(UpdateUserInfoCommand command)
        {
            var requester = await _userManager.FindByIdAsync(command.RequesterId.ToString());

            if (requester == null)
                throw new PermissionDeniedException();

            var requestRole = await _userManager.GetRolesAsync(requester);

            if (requester.Id != command.Id && !requestRole.Contains(RoleConstants.AdminRole))
                throw new PermissionDeniedException();

            var user = requester.Id == command.Id
                ? requester : await _userManager.FindByIdAsync(command.RequesterId.ToString());

            user.UpdateInfo(command.FirstName, command.LastName, command.PhoneNumber, command.Email);

            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }


    }
}
