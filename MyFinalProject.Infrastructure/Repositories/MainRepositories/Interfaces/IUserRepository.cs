using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> FindByUsername(string username);

        Task<User?> FindByEmail(string email);

        Task<User?> GetUserWithPhoneNumber(string phoneNumber);

        Task<User?> GetUserWithCompany(Guid employerId);

        Task<List<User>> GetUsersByRole(string userRole);

        Task<bool> IsEmailUnique(string email);

        Task<User?> GetUserByResumes(Guid jobSeekerId);

        Task<int> GetCountByRole(string userRole);

        Task<int> GetPendingEmployersCount();
    }
}
