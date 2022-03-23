using Domain.Request;
using Infraestructure.Abstractions;
using Infraestructure.BlobStorage;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;

namespace Infraestructure.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobStorageSettings _settings;

        public BlobStorageService(IOptions<BlobStorageSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<string> UploadFile(UploadFileRequest request)
        {
            CloudStorageAccount storageacc = CloudStorageAccount.Parse(_settings.ConnectionString);
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(_settings.Container);
            BlobRequestOptions requestOptions = new() { RetryPolicy = new NoRetry() };
            await container.CreateIfNotExistsAsync(requestOptions, null);

            CloudBlockBlob blob = container.GetBlockBlobReference(request.FileName);
            blob.Properties.ContentType = request.ContentType;
            byte[] imageBytes = Convert.FromBase64String(request.ImageDataBase64);
            
            using var stream = new MemoryStream(imageBytes);
            await blob.UploadFromStreamAsync(stream);

            return blob.StorageUri.PrimaryUri.AbsoluteUri;
        }
    }
}
