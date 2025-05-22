using BulletinBoard.Core.Interfaces;
using BulletinBoard.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BulletinBoard.Core.Extensions
{
    public static class InfrastructureRegistration
    {
        public static void InfrastructureRegister(this IServiceCollection services, string connectionString)
        {
            _ = services.AddScoped<IAdRepository, AdRepository>(_ => new AdRepository(connectionString));
            _ = services.AddScoped<ICategoryRepository, CategoryRepository>(_ => new CategoryRepository(connectionString));
        }
    }
}