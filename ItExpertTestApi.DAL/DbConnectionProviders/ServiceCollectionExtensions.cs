using Microsoft.Extensions.DependencyInjection;

namespace ItExpertTestApi.DAL.DbConnectionProviders
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNpgsqlDbConnectionProvider(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddSingleton<IDbConnectionProvider>(
                _ => new NpgsqlConnectionProvider(connectionString));
            return services;
        }
    }
}
