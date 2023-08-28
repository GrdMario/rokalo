namespace Rokalo
{
    using Hellang.Middleware.ProblemDetails;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Rokalo.Application;
    using Rokalo.Infrastructure.Db.Users;
    using Rokalo.Infrastructure.Email;
    using Rokalo.Infrastructure.Email.Configurations;
    using Rokalo.Presentation.Api;

    internal sealed class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public MssqlSettings MssqlSettings =>
            Configuration
            .GetSection(MssqlSettings.Key)
            .Get<MssqlSettings>();

        public SmtpConfiguration SmtpConfiguration =>
            Configuration
            .GetSection(SmtpConfiguration.Key)
            .Get<SmtpConfiguration>();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddInfrastructureUsersConfiguration(MssqlSettings);
            services.AddInfrastructureEmailConfiguration(SmtpConfiguration);
            services.Configure<SmtpConfiguration>(Configuration.GetSection(SmtpConfiguration.Key));
            services.AddApplicationLayer();
            services.AddPresentationConfiguration(Environment);
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseProblemDetails();

            if (!Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.MigrateMssqlDb();

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
