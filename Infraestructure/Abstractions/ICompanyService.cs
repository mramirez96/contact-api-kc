using Domain;

namespace Infraestructure.Abstractions
{
    public interface ICompanyService
    {
        Task<Company> Get(string name);
        Task<Company> Get(int id);
        Task<Company> Create(string name);
        Task<Company> Update(Company company);
        
    }
}
