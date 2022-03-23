using AutoMapper;
using Domain.Exceptions;
using Infraestructure.Data.Abstractions;
using Infraestructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data.Repositories
{
    internal abstract class RepositoryBase<T> where T : EntityBase
    {
        private readonly ContactContext _context;
        protected readonly IMapper _mapper;

        public RepositoryBase(ContactContext contactContext, IMapper mapper)
        {
            _context = contactContext;
            _mapper = mapper;
        }

        protected DbSet<T> GetSet() => _context.Set<T>();
        protected abstract IQueryable<T> GetSetWithRelations();

        protected async Task<IList<T>> GetAll()
        {
            return await GetSet().ToListAsync();
        }

        protected async Task<T> GetById(int id)
        {
            var entity = await GetSetWithRelations().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new NotFoundException("Contact", $"id {id}");
            return entity;
        }

        protected async Task<T> Add(T entity)
        {
            GetSet().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        protected async Task<T> Update(T entity)
        {
            GetSet().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        protected async Task Delete(int id)
        {
            var entity = await GetById(id);
            GetSet().Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
