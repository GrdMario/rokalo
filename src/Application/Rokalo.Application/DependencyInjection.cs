﻿namespace Rokalo.Application
{
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Rokalo.Application.Internal.Behaviors;
    using System.Reflection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationConfiguration(
            this IServiceCollection services,
            params Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);

            services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
