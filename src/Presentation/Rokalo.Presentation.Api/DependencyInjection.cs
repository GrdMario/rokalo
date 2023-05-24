namespace Rokalo.Presentation.Api
{
    using Hellang.Middleware.ProblemDetails;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Rokalo.Application.Internal.Exceptions;
    using System;
    using System.Linq;
    using ProblemDetailsOptions = Hellang.Middleware.ProblemDetails.ProblemDetailsOptions;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationConfiguration(this IServiceCollection services, IHostEnvironment environment)
        {
            Action<RouteOptions> routeOptions = options => options.LowercaseUrls = true;
            Action<ProblemDetailsOptions> problemDetailsOptions = options => SetProblemDetailsOptions(options, environment);
            Action<MvcNewtonsoftJsonOptions> newtonsoftOptions = options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            };

            services
                .AddRouting(routeOptions)
                .AddProblemDetails(problemDetailsOptions)
                .AddControllers()
                .AddNewtonsoftJson(newtonsoftOptions);

            services.AddSwaggerGen();

            return services;
        }

        private static void SetProblemDetailsOptions(ProblemDetailsOptions options, IHostEnvironment env)
        {
            Type[] knownExceptionTypes = new Type[] { typeof(ServiceValidationException) };

            options.IncludeExceptionDetails = (_, exception) => 
                env.IsDevelopment() &&
                !knownExceptionTypes.Contains(exception.GetType());

            options.Map<ServiceValidationException>(exception =>
            new ValidationProblemDetails(exception.Errors)
            {
                Title = exception.Title,
                Detail = exception.Detail,
                Status = StatusCodes.Status400BadRequest
            });
        }
    }
}
