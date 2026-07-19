
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyFinalProject.Application;
using MyFinalProject.Application.Services.MainServices;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos;
using System.Reflection;
using System.Text;

namespace WebLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddOpenApi();
 //           builder.Services.AddSwaggerGen(option =>
 //           {
 //               var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
 //               var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
 //               option.IncludeXmlComments(xmlPath);

 //               option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
 //               {
 //                   Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
 //                     Enter 'Bearer' [space] and then your token in the text input below.
 //                     \r\n\r\nExample: 'Bearer 12345abcdef'",
 //                   Name = "Authorization",
 //                   In = ParameterLocation.Header,
 //                   Scheme = "Bearer"
 //               });

 //               option.AddSecurityRequirement(new OpenApiSecurityRequirement()
 //   {
 //       {
 //           new OpenApiSecurityScheme
 //           {
 //               Reference = new OpenApiReference
 //               {
 //                   Type = ReferenceType.SecurityScheme,
 //                   Id = "Bearer"
 //               },
 //               Scheme = "oauth2",
 //               Name = "Bearer",
 //               In = ParameterLocation.Header,

 //           },
 //           new List<string>()
 //       }
 //   });
 //});

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
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<FinalDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services
                   .AddIdentity<User, IdentityRole<Guid>>(options =>
                   {
                       options.Password.RequiredLength = 20;
                       options.Password.RequireDigit = true;
                       options.Password.RequireNonAlphanumeric = false;
                       options.Password.RequireUppercase = true;
                       options.Password.RequireLowercase = true;

                       options.Lockout.MaxFailedAccessAttempts = 4;
                       options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                   })
                   .AddEntityFrameworkStores<FinalDbContext>()
                   .AddDefaultTokenProviders();

            var jwtSettings = builder.Configuration.GetSection("JwtConfigurations").Get<JwtSettings>()!;
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtConfigurations"));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ValidAudience = jwtSettings.Audience,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateIssuer = true
                };
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            builder.Services.AddScoped<IRequestResumeRepository, RequestResumeRepository>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();
            builder.Services.AddScoped<IRequestResumeService, RequestResumeService>();
            builder.Services.AddMemoryCache();

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
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
