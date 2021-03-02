using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using XeroChallenge.Domain.Repositories;
using XeroChallenge.Infrastructure.Persistence.DBContext;
using XeroChallenge.Infrastructure.Persistence.Repositories;

namespace XeroChallenge.Infrastructure.Persistence.Extensions
{
    /// <summary>
    /// Extensions for configuring the dependency injection context.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            // Context
            services.AddDbContext<DatabaseContext>(options);

            // Repositories
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
