using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Constants;

namespace FinalProject_MVC.Areas.Admin.Controllers.Base
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.AdminRole)]
    public class AdminBaseController : Controller
    {
    }
}
