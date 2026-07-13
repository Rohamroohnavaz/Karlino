using Microsoft.AspNetCore.Identity;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Domain.Interfaces;
using MyFinalProject.Infrastructure.Repositories.Generics;
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


    }
}
