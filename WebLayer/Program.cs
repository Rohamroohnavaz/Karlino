
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Domain.Interfaces;
using MyFinalProject.Infrastructure;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.Repositories.MainRepositories;

namespace WebLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContext<FinalDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services
                   .AddIdentity<User, IdentityRole<Guid>>()
                   .AddEntityFrameworkStores<FinalDbContext>()
                   .AddDefaultTokenProviders();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.UseCors("AllowAll");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
