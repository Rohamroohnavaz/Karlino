using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Infrastructure.DTO;
using WebLayer.Models;

namespace WebLayer.Controllers
{
    [ApiController]
    [Route("api/companies")]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("/GetMyCompany")]
        [Authorize(Roles = RoleConstants.EmployerRole)]
        public async Task<IActionResult> GetMyCompany()
        {
            var company = await _companyService.GetMyCompanyAsync();

            if(company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpPost("/CreateCompany")]
        [Authorize(Roles = RoleConstants.EmployerRole)]
        public async Task<IActionResult> AddNewCompany([FromBody] CreateCompanyDto dto,
            [FromRoute] Guid companyId)
        {
            await _companyService.CreateNewCompanyAsync(dto, companyId);
            return Ok(ResponseDto.Success());
        }
    }
}
