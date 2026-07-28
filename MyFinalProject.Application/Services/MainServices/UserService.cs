using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Application.Commands;
using MyFinalProject.Application.Commands.ViewModels;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.ServiceExceptions;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.Abstraction;
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
        private readonly ICurrentUserService _currentUserService;

        public UserService(UserManager<User> userManager
            , IUnitOfWork unitOfWork
            , ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task AddVipEmployerForAdv(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
                throw new UserNotFoundException($"{nameof(user)} not found !!"); 

            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("VipEmployer","true"));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateProfileUser(UpdateUserProfileDto dto)
        {
            var currentUser = await _currentUserService.GetAndEnsureApprovedAsync();

            currentUser.UserName = dto.UserName;
            currentUser.Email = dto.Email;
            currentUser.PhoneNumber = dto.Phonenumber;

            await _userManager.UpdateAsync(currentUser);
            await _unitOfWork.SaveChangesAsync();
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

        public async Task ApproveUserAsync(Guid userId)
        {
            var admin = await _currentUserService.GetAndEnsureApprovedAsync();

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new UserNotFoundException("");

            user.IsApproved = true;
            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        //Admin Task
        public async Task ApproveUserWithCheckAsync(Guid userId)
        {
            var requesterId = _currentUserService.UserId;

            var requester = await _userManager.FindByIdAsync(requesterId.ToString());
            if (requester is null)
                throw new UserNotFoundException("User Not Found !!");

            var requesterRoles = await _userManager.GetRolesAsync(requester);
            var isAdmin = requesterRoles.Any(r => r == RoleConstants.AdminRole);

            if (!isAdmin)
                throw new PermissionDeniedException();

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new UserNotFoundException("User Not Found !!");

            user.IsApproved = true;

            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UsersViewModel> GetJobSeekerProfile(Guid userId)
        {
            var id = _currentUserService.UserId;

            var user = await _userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
                throw new UserNotFoundException("Intended User Not Found !!");

            return new UsersViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                CompanyId = user.CompanyId,
                IsApproved = user.IsApproved,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
