using MyFinalProject.Application.DTOs;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.ServiceInterfaces
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetMyCompanyAsync();
        
        Task CreateNewCompanyAsync(CreateCompanyDto dto);

        Task<List<CompanyDto>> GetAllActiveCompanies();
    }
}
