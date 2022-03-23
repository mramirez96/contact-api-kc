using Infraestructure.Abstractions;
using Infraestructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class IoC
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IBlobStorageService, BlobStorageService>();
        }
    }
}
