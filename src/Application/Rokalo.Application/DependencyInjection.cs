namespace Rokalo.Application
{
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationConfiguration(
            this IServiceCollection services,
            params Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);

            services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);
            return services;
        }
    }
}
