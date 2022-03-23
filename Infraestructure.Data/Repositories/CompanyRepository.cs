using AutoMapper;
using Domain;
using Infraestructure.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data.Repositories
{
    internal class CompanyRepository : RepositoryBase<Entities.Company>, ICompanyRepository
    {
        public CompanyRepository(ContactContext context, IMapper mapper)
            : base(context, mapper)
        {

        }

        private Entities.Company Map(Company entity) => _mapper.Map<Entities.Company>(entity);
        private Company Map(Entities.Company entity) => _mapper.Map<Company>(entity);

        protected override IQueryable<Entities.Company> GetSetWithRelations() => GetSet().AsNoTracking();

        public async Task<Company> Add(Company entity)
        {
            var dbEntity = Map(entity);
            var added = await Add(dbEntity);
            return Map(added);
        }

        public async Task<Company> Update(Company entity)
        {
            var dbEntity = Map(entity);
            var updated = await Update(dbEntity);
            return Map(updated);
        }

        async Task<Company> IRepository<Company>.GetById(int id)
        {
            return Map(await GetById(id));
        }

        async Task IRepository<Company>.Delete(int id)
        {
            await Delete(id);
        }

        async Task<IList<Company>> IRepository<Company>.GetAll()
        {
            // TODO : esto modificarlo para que o devuelva un queryable o reciba parametros para filtrar
            return (await GetAll()).Select(Map).ToList();
        }

        public async Task<Company> GetByName(string name)
        {
            return Map(await GetSet().AsNoTracking().FirstOrDefaultAsync(x => x.Name == name));
        }
    }
}
