using System.Data;
using System.Data.SqlClient;
using Gateway.Data.Repository;
using MicroOrm.Dapper.Repositories.Config;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Data;

public static class ServiceExtension
{
    public static void RegisterData(this IServiceCollection services, IConfiguration configuration)
    {
        MicroOrmConfig.SqlProvider = SqlProvider.MSSQL;
        MicroOrmConfig.AllowKeyAsIdentity = true;
        services.AddScoped<IGatewayRepository, GatewayRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentHistoryRepository, PaymentHistoryRepository>();
        services.AddTransient<IDbConnection>(_ =>
            new SqlConnection(configuration.GetConnectionString("PaymentConnectionString")));
        services.AddSingleton(typeof(ISqlGenerator<>), typeof(SqlGenerator<>));
    }
}