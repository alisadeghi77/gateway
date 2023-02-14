using System;
using Dapper.Contrib.Extensions;
using Gateway.Data.Entities.Enums;

namespace Gateway.Data.Entities;

[Table("PaymentHistoryLogs")]
public class PaymentHistory
{
    public Guid Id { get; set; }
    public long PaymentId { get; set; }
    public int TenantId { get; set; }
    public string Reference { get; set; }
    public string TrackingCode { get; set; }
    public string BankTrackingCode { get; set; }
    public long Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}