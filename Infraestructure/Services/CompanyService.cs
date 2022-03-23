using Domain;
using Infraestructure.Abstractions;
using Infraestructure.Data.Abstractions;

namespace Infraestructure.Services
{
    internal class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _repository = companyRepository;
        }

        public async Task<Company> Create(string name)
        {
            return await _repository.Add(new Company { Name = name });
        }

        public async Task<Company> Get(string name)
        {
            return await _repository.GetByName(name);
        }

        public async Task<Company> Get(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Company> Update(Company company)
        {
            return await _repository.Update(company);
        }
    }
}
