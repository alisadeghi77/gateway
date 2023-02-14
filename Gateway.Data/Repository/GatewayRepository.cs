using System.Data;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Gateway.Data.Repository;

public interface IGatewayRepository : IDapperRepository<Entities.Gateway>
{
}

public class GatewayRepository : DapperRepository<Entities.Gateway> , IGatewayRepository
{
    public GatewayRepository(IDbConnection connection, ISqlGenerator<Entities.Gateway> sqlGenerator)
        : base(connection, sqlGenerator)
    {
    }
}