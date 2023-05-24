﻿namespace Rokalo
{
    using Hellang.Middleware.ProblemDetails;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Rokalo.Application;
    using Rokalo.Infrastructure.Db.Users;
    using Rokalo.Presentation.Api;
    using System;

    internal sealed class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public PostgreSettings postgreSettings =>
            Configuration
            .GetSection(PostgreSettings.Key)
            .Get<PostgreSettings>() ?? throw new ArgumentException(nameof(PostgreSettings));

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            //TODO services.AddInfrastructureUsersConfiguration(PostgreSettings);
            services.AddApplicationConfiguration();
            services.AddPresentationConfiguration(Environment);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseProblemDetails();

            if (!Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}