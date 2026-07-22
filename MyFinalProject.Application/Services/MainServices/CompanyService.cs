using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.ServiceExceptions;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.DTO;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.RepoExceptions;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.MainServices
{
    public class CompanyService : ICompanyService
    {
        private readonly ICurrentUserService _currentUser;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(ICurrentUserService currentUser 
            ,ICompanyRepository companyRepository
            ,IUnitOfWork unitOfWork)
        {
            _currentUser = currentUser;
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateNewCompanyAsync(CreateCompanyDto dto, Guid companyId)
        {
            var existCompany = await _companyRepository.ExistCompanyAsync(companyId);

            if (existCompany)
                throw new InvalidCompanyException("This Company Is Already Exist !");

            var newCompany = new Company(dto.CompanyName, dto.CompanyLocation, dto.Province, dto.City);
            
            await _companyRepository.AddAsync(newCompany);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<CompanyDto>> GetAllActiveCompanies()
        {
            var result = await _companyRepository.GetAllExistCompanies();

            return result.Select(c => new CompanyDto
            {
                Id = c.Id,
                CompanyName = c.CompanyName,
                CompanyLocation = c.CompanyLocation,
                Province = c.Province,
                City = c.City
            }).ToList();
        }

        public async Task<CompanyDto> GetMyCompanyAsync()
        {
            var company = await _companyRepository.GetCompanyByUserId(_currentUser.UserId);

            if (company == null)
                throw new CompanyNotFoundException();

            return new CompanyDto
            {
                Id = company.Id,
                CompanyName = company.CompanyName,
                CompanyLocation = company.CompanyLocation,
                Province = company.Province,
                City = company.City
            };
        }
    }
}
