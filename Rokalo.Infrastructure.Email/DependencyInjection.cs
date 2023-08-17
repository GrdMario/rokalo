namespace Rokalo.Infrastructure.Email
{
    using Microsoft.Extensions.DependencyInjection;
    using Rokalo.Application.Contracts;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureEmailConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }

    }
}
