using AutoMapper;
using Domain;
using Domain.Exceptions;
using Domain.Request;
using Infraestructure.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data.Repositories
{
    internal class ContactRepository : RepositoryBase<Entities.Contact>, IContactRepository
    {
        private readonly ICompanyRepository _companyRepository;

        public ContactRepository(ContactContext context, ICompanyRepository companyRepository, IMapper mapper)
            : base(context, mapper)
        {
            _companyRepository = companyRepository;
        }

        private Entities.Contact Map(Contact entity) => _mapper.Map<Entities.Contact>(entity);
        private Contact Map(Entities.Contact entity) => _mapper.Map<Contact>(entity);

        protected override IQueryable<Entities.Contact> GetSetWithRelations() => GetSet()
            .AsNoTracking()
            .Include(x => x.Phones)
            .Include(x => x.Address)
            .Include(x => x.Company);

        public async Task<Contact> Add(Contact entity)
        {
            var dbEntity = Map(entity);

            if (dbEntity.Company != null && dbEntity.Company.Id != 0)
            {
                dbEntity.CompanyId = dbEntity.Company.Id;
                dbEntity.Company = null;
            }
            else if (await _companyRepository.GetByName(dbEntity.Company.Name) != null)
                throw new ConflictException("company", $"name {dbEntity.Company.Name}");

            var added = Map(await Add(dbEntity));
            added.Company = await _companyRepository.GetById(dbEntity.CompanyId);
            return added;
        }

        public async Task<Contact> Update(Contact entity)
        {
            var dbEntity = Map(entity);
            var updated = await Update(dbEntity);
            return Map(updated);
        }

        async Task<Contact> IRepository<Contact>.GetById(int id)
        {
            return Map(await GetById(id));
        }

        async Task IRepository<Contact>.Delete(int id)
        {
            await Delete(id);
        }

        async Task<IList<Contact>> IRepository<Contact>.GetAll()
        {
            return (await GetAll()).Select(Map).ToList();
        }

        public async Task<Contact> GetByEmail(string email)
        {
            var entity = await GetSetWithRelations().FirstOrDefaultAsync(x => x.Email == email);
            if (entity == null)
                throw new NotFoundException("Contact", $"email {email}");
            return Map(entity);
        }

        public async Task<Contact> GetByPhone(string phoneNumber)
        {
            var entity = await GetSetWithRelations().FirstOrDefaultAsync(x => x.Phones.Any(p => p.Number == phoneNumber));
            if (entity == null)
                throw new NotFoundException("Contact", $"phoneNumber {phoneNumber}");
            return Map(entity);
        }

        public IList<Contact> Search(SearchContactRequest request)
        {
            var entities = GetSetWithRelations()
                .Where(c => string.IsNullOrEmpty(request.Country) || c.Address.Country == request.Country)
                .Where(c => string.IsNullOrEmpty(request.City) || c.Address.City == request.City);

            if (!entities.Any())
                throw new NotFoundException("Contacts",
                    $"country {request.Country} city {request.City}");

            return entities.Select(Map).ToList();
        }
    }
}
