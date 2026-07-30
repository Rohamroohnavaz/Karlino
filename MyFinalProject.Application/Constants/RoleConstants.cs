using MyFinalProject.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Constants
{
    public static class RoleConstants
    {
        public const string JobSeekerRole = "JobSeeker";
        public const string EmployerRole = "Employer";
        public const string AdminRole = "Admin";

        public static string ToName(UserRole role) => role switch
        {
            UserRole.JobSeeker => JobSeekerRole,
            UserRole.Employer => EmployerRole,
            UserRole.Admin => AdminRole,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }
}
