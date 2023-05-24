namespace Rokalo.Infrastructure.Db.Users
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureUsersConfiguration(this IServiceCollection services, PostgreSettings settings)
        {
            // TODO
            // Add DbContext, repositories, IUnitOfWork

            return services;
        }
    }

    public class PostgreSettings
    {
        public const string Key = nameof(PostgreSettings);
        public string ConnectionString { get; set; } = default!;
    }
}
