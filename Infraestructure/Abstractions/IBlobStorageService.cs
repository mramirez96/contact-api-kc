using Domain.Request;

namespace Infraestructure.Abstractions
{
    internal interface IBlobStorageService
    {
        Task<string> UploadFile(UploadFileRequest request);
    }
}
