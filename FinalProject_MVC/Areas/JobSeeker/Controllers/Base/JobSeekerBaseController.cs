using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Constants;

namespace FinalProject_MVC.Areas.JobSeeker.Controllers.Base
{
    [Area("JobSeeker")]
    [Authorize(Roles = RoleConstants.JobSeekerRole)]
    public class JobSeekerBaseController : Controller
    {
    }
}
