using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace FeederSokML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(!int.TryParse(args[0], out var maxThreadCount))
            {
                throw new System.Exception("Nie podano lub nieprawidłowa wartość w pierwszym parametrze (liczba wątków)");
            };

            var services = new ServiceCollection();

            ConfigureServices(services);

            services.BuildServiceProvider()
                .GetService<Executor>()
                .Execute(maxThreadCount);
        }

        static void ConfigureServices(IServiceCollection services)
        {
            var config = LoadAppSettings();

            services.AddSingleton<Executor, Executor>();
            services.AddLogger();
            services.AddDbContext(config.GetConnectionString("SOK_DB"));
            services.AddServices();
            services.AddRepositories();
        }

        static IConfigurationRoot LoadAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}