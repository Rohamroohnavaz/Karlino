using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> FindByName(string firstName);

        Task<User?> FidnByEmail(string email);

        Task<User?> GetUserWithPhoneNumber(string phoneNumber);
    }
}
