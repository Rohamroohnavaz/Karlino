using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Domain.Exceptions;
using MyFinalProject.Infrastructure.DTO;
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
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UserRepository(FinalDbContext dbContext
            , UserManager<User> userManager
            , RoleManager<IdentityRole<Guid>> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
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
            (Expression<Func<User, TResult>> projection) where TResult : UserViewModel
        {
            return await _dbContext.Users
                .Select(projection)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();
        }

        public async Task<User?> GetUserWithCompany(Guid employerId)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.CompanyId == employerId);
        }

        public async Task<User?> GetUserByResumes(Guid jobSeekerId)
        {
            return await _dbContext.Users
                .Include(u => u.RequestResumes)
                .FirstOrDefaultAsync(u => u.Id == jobSeekerId);
        }

        public async Task<int> GetCountByRole(string userRole)
        {
            var role = await _roleManager.FindByNameAsync(userRole);
            if (role == null)
                return 0;


            return await (from user in _userManager.Users
                          join roleUser in _dbContext.UserRoles on user.Id equals roleUser.UserId
                          where roleUser.RoleId == role.Id && !user.IsDeleted
                          select user).CountAsync();
        }

        public async Task<int> GetPendingEmployersCount()
        {
            var employer = await _roleManager.FindByNameAsync("Employer");
            if (employer is null)
                return 0;

            return await (from user in _userManager.Users
                          join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
                          where userRole.RoleId == employer.Id
                                && !user.IsApproved
                                && !user.IsDeleted
                          select user).CountAsync();
        }

        public async Task<List<AdminEmployerTableDto>> GetPendingEmployersAsync()
        {
            return await _dbContext.Users
                .Where(u => u.IsApproved == false
                && u.IsDeleted == false
                && u.Role == UserRole.Employer)
                .OrderBy(u => u.ModifiedAt)             
                .Select(u => new AdminEmployerTableDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    CompanyName = u.Company.CompanyName,
                    RegisteredAt = u.RegisteredAt
                }).ToListAsync();
        }

        public async Task<bool> SetEmployerApprovalAsync(Guid id, bool approved)
        {
            var employer = await _dbContext.Users.FindAsync(id);

            if (employer == null)
                return false;

            employer.IsApproved = approved;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<AdminUserTableDto>> GetAllUsersForAdminAsync()
        {
            return await _dbContext.Users
                .Select(u => new AdminUserTableDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    FullName = u.FirstName + " " + u.LastName,
                    RoleName = u.Role != null ? u.Role.ToString() : "بدون نقش"
                })
                .ToListAsync();
        }
    }
}
