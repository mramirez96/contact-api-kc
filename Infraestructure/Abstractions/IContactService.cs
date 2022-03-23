using Domain;
using Domain.Request;

namespace Infraestructure.Abstractions
{
    public interface IContactService
    {
        Task<Contact> Create(Contact contact);
        Task<Contact> Update(Contact contact);
        Task<Contact> Get(int id);
        Task<Contact> Get(string email);
        Task<Contact> GetByPhone(string phoneNumber);
        Task Delete(int id);
        IList<Contact> Search(SearchContactRequest request);
    }
}
