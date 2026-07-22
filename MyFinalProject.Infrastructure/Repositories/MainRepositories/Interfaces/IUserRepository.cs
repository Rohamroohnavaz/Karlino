using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
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
        Task<User?> FindByName(string firstName);

        Task<User?> FindByEmail(string email);

        Task<User?> GetUserWithPhoneNumber(string phoneNumber);

        Task<List<User>> GetUsersByRole(string userRole);

        Task<bool> IsEmailUnique(string email);
    }
}
