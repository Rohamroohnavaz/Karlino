using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Infrastructure.DTO;

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

        [HttpGet("/GetMyCompany")]
        public async Task<IActionResult> GetMyCompany()
        {
            var company = await _companyService.GetMyCompanyAsync();

            if(company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpPost("/CreateCompany")]
        public async Task<IActionResult> AddNewCompany([FromBody] CreateCompanyDto dto,
            [FromRoute] Guid companyId)
        {
            await _companyService.CreateNewCompanyAsync(dto, companyId);
            return Ok();
        }
    }
}
