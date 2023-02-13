using System.Data;
using System.Data.SqlClient;
using Gateway.Data.Repository;
using MicroOrm.Dapper.Repositories.Config;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Data.Extensions;

public static class StartupExtension
{
    public static void RegisterData(this IServiceCollection services, IConfiguration configuration)
    {
        MicroOrmConfig.SqlProvider = SqlProvider.MSSQL;
        MicroOrmConfig.AllowKeyAsIdentity = true;
        services.AddScoped<PaymentRepository>();
        services.AddSingleton(typeof(ISqlGenerator<>), typeof(SqlGenerator<>));
        services.AddTransient<IDbConnection>(_ =>
            new SqlConnection(configuration.GetConnectionString("PaymentConnectionString")));
    }
}
