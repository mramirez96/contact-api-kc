namespace Infraestructure.Data.Abstractions
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(int id);
    }
}
