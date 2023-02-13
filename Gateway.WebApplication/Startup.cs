using Gateway.Services.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Gateway.WebFramework.Extensions;
using Gateway.WebFramework.Middlewares;
using Parbad.Builder;
using Parbad.Gateway.Mellat;
using Parbad.Gateway.ParbadVirtual;

namespace Gateway.WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterStartupDependencies(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAdvancedExceptionHandler();
            app.UseRouting();
            app.UseCustomSwagger();
            app.UseParbadVirtualGateway();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
