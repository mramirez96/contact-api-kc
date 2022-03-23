using Domain.Request;

namespace Infraestructure.Abstractions
{
    public interface IBlobStorageService
    {
        Task<string> UploadFile(UploadFileRequest request);
    }
}
