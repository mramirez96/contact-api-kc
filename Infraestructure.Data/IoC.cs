using Infraestructure.Data.Abstractions;
using Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Data
{
    public static class IoC
    {
        public static void AddContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ContactContext>(options => 
                //options.UseMySQL(connectionString),
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)),
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped);

            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
        }
    }
}
