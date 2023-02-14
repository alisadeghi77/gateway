using System.Data;
using Gateway.Data.Entities;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Gateway.Data.Repository;

public interface IPaymentHistoryRepository : IDapperRepository<PaymentHistory>
{
}

public class PaymentHistoryRepository : DapperRepository<PaymentHistory>, IPaymentHistoryRepository
{
    public PaymentHistoryRepository(IDbConnection connection, ISqlGenerator<PaymentHistory> sqlGenerator)
        : base(connection, sqlGenerator)
    {
    }
}