using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.ServiceInterfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }

        Guid CompanyId { get; }

        string? Username { get; }

        string? Role { get; }
    }
}
