using Gateway.Data.Repository;
using MicroOrm.Dapper.Repositories.Config;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders.Testing;

namespace Gateway.Data;

public static class ServiceExtension
{
    public static void RegisterData(this IServiceCollection services)
    {
        MicroOrmConfig.SqlProvider = SqlProvider.MSSQL;
        MicroOrmConfig.AllowKeyAsIdentity = true;
        services.AddSingleton(typeof(ISqlGenerator<>), typeof(SqlGenerator<>));
        services.AddScoped<PaymentRepository>();
    }
}