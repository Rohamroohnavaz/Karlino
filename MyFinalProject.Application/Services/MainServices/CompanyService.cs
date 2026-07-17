using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.MainServices
{
    public class CompanyService : ICompanyService
    {
        public Task<Company> GetMyCompanyAsync()
        {
            throw new NotImplementedException();
        }
    }
}
