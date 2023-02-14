using System.Data;
using Gateway.Data.Entities;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Gateway.Data.Repository;

public interface IPaymentRepository : IDapperRepository<Payment>
{
}

public class PaymentRepository : DapperRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(IDbConnection connection, ISqlGenerator<Payment> sqlGenerator)
        : base(connection, sqlGenerator)
    {
    }
}