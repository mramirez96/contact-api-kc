namespace Infraestructure.Data.Abstractions
{
    public interface ICompanyRepository : IRepository<Domain.Company>
    {
        Task<Domain.Company> GetByName(string name);
    }
}
