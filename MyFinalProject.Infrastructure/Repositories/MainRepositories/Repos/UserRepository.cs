using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Domain.Exceptions;
using MyFinalProject.Infrastructure.RepoExceptions;
using MyFinalProject.Infrastructure.Repositories.Generics;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly FinalDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public UserRepository(FinalDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<User?> FindByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User?> FindByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<List<User>> GetUsersByRole(string userRole)
        {
            if (Enum.TryParse<UserRole>(userRole, out var enumRole))
            {
                return await _dbContext.Users
                    .Where(u => u.Role == enumRole)
                    .ToListAsync();
            }

            return new List<User>();
        }

        public async Task<User?> GetUserWithPhoneNumber(string phoneNumber)
        {
            var findUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

            if (findUser is null)
                throw new InvalidUserException("We can't find !!");

            return findUser;
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            return await _dbContext.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<List<TResult>> GetUsersAsViewModel<TResult>
            (Expression<Func<User ,TResult>> projection) where TResult : UserViewModel
        {
            return await _dbContext.Users
                .Select(projection)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();
        }
    }
}
