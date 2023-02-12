using System;

namespace Gateway.Data.Entities;

public class PaymentHistory
{
    public long Id { get; set; }
    public int TenantId { get; set; }
    public string Reference { get; set; }
    public string TrackingCode { get; set; }
    public string BankTrackingCode { get; set; }
    public long Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}