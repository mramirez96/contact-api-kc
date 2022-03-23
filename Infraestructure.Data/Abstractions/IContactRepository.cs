using Domain;
using Domain.Request;

namespace Infraestructure.Data.Abstractions
{
    public interface IContactRepository : IRepository<Domain.Contact>
    {
        Task<Contact> GetByEmail(string email);
        Task<Contact> GetByPhone(string phoneNumber);
        IList<Contact> Search(SearchContactRequest request);
    }
}
