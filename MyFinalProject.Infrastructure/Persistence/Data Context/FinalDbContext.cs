using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure
{
    public class FinalDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public FinalDbContext(DbContextOptions<FinalDbContext> options) : base(options)
        {
           
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<RequestResume> Resumes { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Attach> Attaches { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<RevokedToken> RevokedTokens { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
