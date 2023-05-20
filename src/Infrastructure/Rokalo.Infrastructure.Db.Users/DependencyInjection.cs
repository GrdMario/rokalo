namespace Rokalo.Infrastructure.Db.Users
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
        public const String Key = nameof(PostgreSettings);
        public string ConnectionString { get; set; } = default;
    }
}
