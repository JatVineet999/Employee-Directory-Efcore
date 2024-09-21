using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repos;
using Infrastructure.Interfaces;

namespace Infrastructure
{
    public class ServiceExtensions
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDepartmentsRepo, DepartmentsRepo>();
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
        }
    }
}

