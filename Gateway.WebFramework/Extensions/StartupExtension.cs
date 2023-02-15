using Gateway.Services.Extensions;
using Gateway.Services.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Gateway.ViewModels.Configuration;
using Parbad.Builder;
using Parbad.Gateway.Mellat;
using Parbad.Gateway.ParbadVirtual;

namespace Gateway.WebFramework.Extensions
{
    public static class StartupExtension
    {
        public static void RegisterStartupDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<ApplicationConfiguration>(a => configuration.Bind(a));
            services.RegisterSwagger();
            services.RegisterServices(configuration);
            services.RegisterGateway();
            services.AddHealthChecks();
        }

        private static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Gateway" });
            });
        }

        private static void RegisterGateway(this IServiceCollection services)
        {
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

                    gateways.AddParbadVirtual()
                        .WithOptions(options => options.GatewayPath = "/testgateway");
                })
                .ConfigureHttpContext(httpContextBuilder => httpContextBuilder.UseDefaultAspNetCore())
                .ConfigureStorage(builder => builder.AddStorage<ParbadStorage>(ServiceLifetime.Scoped));
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
