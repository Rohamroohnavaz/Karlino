using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.DTO;
using MyFinalProject.Infrastructure.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<List<Company>> GetAllExistCompanies();

        Task<Company?> GetCompanyByUserId(Guid userId);

        Task<bool> ExistCompanyAsync(Guid companyId);

        Task CreateCompanyAsync(CreateCompanyDto dto);
    }
}
