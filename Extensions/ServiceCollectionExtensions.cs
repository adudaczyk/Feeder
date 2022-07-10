using BlackOps.Config;
using FeederSokML.EntityFramework;
using FeederSokML.EntityFramework.Repositories;
using FeederSokML.Services;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            IConfigurationProvider config = new AppSettingsJsonConfigurerWithoutDB();
            connectionString = config.TokenizeValueWithFile(connectionString);

            services.AddDbContext<SokDbContext>(options => options
            .UseSqlServer(connectionString, o =>
            {
                o.EnableRetryOnFailure();
            }));
        }

        public static void AddLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILogger>(LogManager.GetCurrentClassLogger());
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProcesyRepository, ProcesyRepository>();
            services.AddTransient<IProcesRezultMLRepository, ProcesRezultMLRepository>();
            services.AddTransient<IStoredProceduresRepository, StoredProceduresRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IClassificationService, ClassificationService>();
        }
    }
}
