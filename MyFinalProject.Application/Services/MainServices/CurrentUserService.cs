using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyFinalProject.Application.ServiceExceptions;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.MainServices
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor 
            ,UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public Guid UserId
            => Guid.Parse(_httpContextAccessor.HttpContext!.User
                .FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public Guid CompanyId => Guid.Parse(_httpContextAccessor.HttpContext!.User
        .FindFirst("CompanyId")!.Value);

        public string? Username {  get; set; }

        public string? Role
            => _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Role)?.Value;

        public string? Email => throw new NotImplementedException();

        public async Task<User> GetAndEnsureApprovedAsync()
        {
            var userId = UserId;
            var findUser = await _userManager.FindByIdAsync(userId.ToString());

            if (findUser == null)
                throw new UserNotFoundException("User Not Found !");

            if (!findUser.IsApproved)
                throw new PermissionDeniedException();

            return findUser;
        }
    }
}
