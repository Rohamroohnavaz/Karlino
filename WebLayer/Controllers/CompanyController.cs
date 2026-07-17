using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Services.ServiceInterfaces;

namespace WebLayer.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyCompany()
        {
            var company = await _companyService.GetMyCompanyAsync();

            if(company == null)
                return NotFound();

            return Ok(company);
        }
    }
}
