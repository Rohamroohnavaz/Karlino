using FinalProject_MVC.Helpers;
using FinalProject_MVC.Models;
using FinalProject_MVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.Results;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly IApiService _apiService;
    private readonly ISettingRepository _settingRepository;

    public AccountController(IApiService apiService ,ISettingRepository settingRepository)
    {
        _apiService = apiService;
        _settingRepository = settingRepository;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var loginResult = await _apiService.PostAsync<LoginResponse>("/LoginUser", new
            {
                model.Email,
                model.Password,
                model.RememberMe,
            });

            HttpContext.Session.SetString("Token", loginResult.AccessToken);

            var role = loginResult.Role?.Trim();

            if (string.IsNullOrWhiteSpace(role))
            {
                role = JwtHelper.GetRoleFromJwt(loginResult.AccessToken)?.Trim();
            }

            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Email, model.Email),
               new Claim(ClaimTypes.Name, model.Email),
               new Claim("Token", loginResult.AccessToken),
            };

            if (!string.IsNullOrWhiteSpace(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            if (!string.IsNullOrEmpty(loginResult.CompanyId))
            {
                claims.Add(new Claim("CompanyId", loginResult.CompanyId));
            }

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                authProperties);

            if (string.Equals(role, RoleConstants.AdminRole, StringComparison.OrdinalIgnoreCase))
            {
                return Redirect("/Admin/Dashboard");
            }

            if (string.Equals(role, RoleConstants.EmployerRole, StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "Home");
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Login failed: " + ex.Message);
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        if (!await IsRegistrationOpenAsync())
        {
            ModelState.AddModelError("", "ثبت‌نام در حال حاضر غیرفعال است.");
            return View("Login", new LoginViewModel());
        }

        return View(new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!await IsRegistrationOpenAsync())
        {
            ModelState.AddModelError("", "ثبت‌نام در حال حاضر غیرفعال است.");
            return View("Login", new LoginViewModel());
        }

        if (!ModelState.IsValid)
            return View(model);

        try
        {
            if (model.IsEmployer)   
            {
                await _apiService.PostAsync<RegisterResult>("/RegisterEmployer", new
                {
                    model.FirstName,
                    model.LastName,
                    model.PhoneNumber,
                    model.Email,
                    model.Password,
                    Username = model.Email,
                    model.CompanyName,
                    model.CompanyLocation,
                    model.Province,
                    model.City
                });
            }
            else
            {
                await _apiService.PostAsync<RegisterResult>("/RegisterJobSeeker", new
                {
                    model.FirstName,
                    model.LastName,
                    model.PhoneNumber,
                    model.Email,
                    model.Password,
                    Username = model.Email
                });
            }

            TempData["Success"] = "ثبت‌نام با موفقیت انجام شد. حالا وارد شوید.";
            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "ثبت‌نام ناموفق بود: " + ex.Message);
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await _apiService.PostAsync<object>("/LogoutUser", null);
        }
        catch { /* Ignore logout API errors */ }

        HttpContext.Session.Remove("Token");

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login");
    }

    private async Task<bool> IsRegistrationOpenAsync()
    {
        var value = await _settingRepository.GetValueAsync("IsRegistrationOpen");
        return !string.Equals(value, "false", StringComparison.OrdinalIgnoreCase);
    }
}