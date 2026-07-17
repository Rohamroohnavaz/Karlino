using Microsoft.AspNetCore.Http;
using MyFinalProject.Application.Services.ServiceInterfaces;
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

        public Guid UserId
            => Guid.Parse(_httpContextAccessor.HttpContext!.User
                .FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public Guid CompanyId => Guid.Parse(_httpContextAccessor.HttpContext!.User
        .FindFirst("CompanyId")!.Value);

        public string? Username {  get; set; }

        public string? Role
            => _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Role)?.Value;

        
    }
}
