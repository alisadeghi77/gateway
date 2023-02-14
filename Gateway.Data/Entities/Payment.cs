using System;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;
using Gateway.Data.Entities.Enums;

namespace Gateway.Data.Entities;

[System.ComponentModel.DataAnnotations.Schema.Table("Payments")]
public class Payment
{
    [Dapper.Contrib.Extensions.Key]
    public long Id { get; set; }
    
    public int TenantId { get; set; }
    
    [MaxLength(36)]
    public string Reference { get; set; }
    
    [MaxLength(36)]
    public string TrackingCode { get; set; }

    [MaxLength(36)]
    public string BankTrackingCode { get; set; }
    
    public long Amount { get; set; }
    
    public DateTime PaymentDate { get; set; }
    
    public PaymentStatus PaymentStatus { get; set; }
    
    public DateTime CreatedDate { get; set; }
}
