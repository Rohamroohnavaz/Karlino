using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Domain.Exceptions;
using MyFinalProject.Infrastructure.Repositories.Generics;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinalDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public UserRepository(FinalDbContext dbContext ,UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<User?> FidnByEmail(string email)
        {
            var findUser = await _userManager.FindByEmailAsync(email);

            if (findUser == null)
                throw new InvalidEmailException("User with this email not found !");

            return findUser;
        }

        public async Task<User?> FindByName(string firstName)
        {
           var findUser = await _userManager.FindByNameAsync(firstName);

            if (findUser is null)
                throw new Exception("We don't have any user with this firstname !");

            return findUser;

        }


    }
}
