using FinalProject_MVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Application.Services.MainServices;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos;
using System;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FinalDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient<IApiService, ApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRequestResumeRepository, RequestResumeRepository>();
builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<ISettingRepository, SettingRepository>();
builder.Services
    .AddIdentityCore<User>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = false;

        options.Lockout.MaxFailedAccessAttempts = 4;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<FinalDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

var cultureInfo = new CultureInfo("fa-IR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "JobSeeker",
    areaName: "JobSeeker",
    pattern: "JobSeeker/{controller=Dashboard}/{action=Index}/{id?}");

//app.MapAreaControllerRoute(
//    name: "JobSeeker",
//    pattern: "JobSeeker/{controller=Dashboard}/{action=Index}/{id?}",
//    defaults: new { area = "JobSeeker" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();