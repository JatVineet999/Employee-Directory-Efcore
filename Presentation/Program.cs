using Microsoft.Extensions.DependencyInjection;
using Presentation.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Infrastructure.Db_Context;


namespace Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("app-settings.json", optional: false, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<EmployeeDirectoryDbContext>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("EmployeeDatabase")), ServiceLifetime.Singleton);

                    Application.ServiceExtensions.ConfigureServices(services);
                    Infrastructure.ServiceExtensions.ConfigureServices(services);
                    ServiceExtensions.ConfigureServices(services);
                });

            using (var host = builder.Build())
            {
                var serviceProvider = host.Services;

                var mainMenuManager = serviceProvider.GetService<IMainMenuManager>();
                mainMenuManager?.DisplayMainMenu();
            }
        }
    }
}
