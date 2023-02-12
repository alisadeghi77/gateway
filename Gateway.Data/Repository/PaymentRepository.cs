using System.Data;
using Gateway.Data.Entities;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Gateway.Data.Repository;

public class PaymentRepository : DapperRepository<Payment>
{
    public PaymentRepository(IDbConnection connection, ISqlGenerator<Payment> sqlGenerator)
        : base(connection, sqlGenerator)
    {
    }
}