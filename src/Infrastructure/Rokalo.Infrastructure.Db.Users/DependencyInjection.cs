namespace Rokalo.Infrastructure.Db.Users
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Rokalo.Application.Contracts;
    using Rokalo.Infrastructure.Db.Users.Repositories;
    using System;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureUsersConfiguration(this IServiceCollection services, MssqlSettings settings)
        {
            services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(settings.ConnectionString));

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUsersUnitOfWork, UsersUnitOfWork>();

            return services;
        }
    }

    public class MssqlSettings
    {
        public const string Key = nameof(MssqlSettings);
        public string ConnectionString { get; set; } = default;
    }
}
