using Domain;
using Domain.Request;
using Infraestructure.Abstractions;
using Infraestructure.Data.Abstractions;

namespace Infraestructure.Services
{
    internal class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IBlobStorageService _blobStorageService;

        public ContactService(IContactRepository contactRepository,
            IBlobStorageService blobStorageService)
        {
            _repository = contactRepository;
            _blobStorageService = blobStorageService;
        }

        private async Task<string> UploadFile(string image)
        {
            var uri = await _blobStorageService.UploadFile(new UploadFileRequest
            {
                FileName = DateTime.Now.ToString("yyyyMMdd") + Guid.NewGuid().ToString() + ".png",
                ImageDataBase64 = image
            });
            return uri;
        }

        public async Task<Contact> Create(Contact contact)
        {
            if (!string.IsNullOrEmpty(contact.ImageAsBase64))
            {
                contact.Uri = await UploadFile(contact.ImageAsBase64);
            }

            return await _repository.Add(contact);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<Contact> Get(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Contact> Get(string email)
        {
            return await _repository.GetByEmail(email);
        }

        public async Task<Contact> GetByPhone(string phoneNumber)
        {
            return await _repository.GetByPhone(phoneNumber);
        }

        public IList<Contact> Search(SearchContactRequest request)
        {
            return _repository.Search(request);
        }

        public async Task<Contact> Update(Contact contact)
        {
            return await _repository.Update(contact);
        }
    }
}
