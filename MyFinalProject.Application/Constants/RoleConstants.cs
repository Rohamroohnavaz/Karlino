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
        public static readonly string JobSeekerRole = "JobSeeker";
        public static readonly string EmployerRole = "Employer";
        public static readonly string AdminRole = "Admin";

        public static string ToName(UserRole role) => role switch
        {
            UserRole.JobSeeker => JobSeekerRole,
            UserRole.Employer => EmployerRole,
            UserRole.Admin => AdminRole,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }
}
