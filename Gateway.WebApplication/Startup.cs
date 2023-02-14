using Gateway.Data;
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

            services.AddParbad()
                .ConfigureGateways(gateways =>
                {
                    gateways.AddMellat()
                        .WithAccounts(accounts =>
                        {
                            accounts.AddInMemory(account =>
                            {
                                account.TerminalId = 134753714;
                                account.UserName = "user134753714";
                                account.UserPassword = "77342348";
                            });
                        })
                        .WithOptions(op =>
                        {
                            op.ApiUrl =
                                "https://sandbox.banktest.ir/mellat/bpm.shaparak.ir/pgwchannel/services/pgw?wsdl";
                            op.PaymentPageUrl =
                                "https://sandbox.banktest.ir/mellat/bpm.shaparak.ir/pgwchannel/startpay.mellat";
                        });

                    gateways
                        .AddParbadVirtual()
                        .WithOptions(options => options.GatewayPath = "/testgateway");
                })
                .ConfigureHttpContext(httpContextBuilder => httpContextBuilder.UseDefaultAspNetCore())
                .ConfigureStorage(builder => builder.AddStorage<ParbadStorage>(ServiceLifetime.Scoped));
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