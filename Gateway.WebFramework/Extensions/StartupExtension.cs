using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Gateway.ViewModels.Configuration;

namespace Gateway.WebFramework.Extensions
{
    public static class StartupExtension
    {
        public static void RegisterStartupDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<ApplicationConfiguration>(a => configuration.Bind(a));
            services.RegisterSwagger();
            services.AddHealthChecks();
        }

        private static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Gateway" });
            });
        }

        public static void UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway API");
            });
        }
    }
}