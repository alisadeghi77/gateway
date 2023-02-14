using System;
using Dapper.Contrib.Extensions;
using Gateway.Data.Entities.Enums;

namespace Gateway.Data.Entities;

[Table("Gateways")]
public class Gateway
{
    public Guid Id { get; set; }
    public BankType BankType { get; set; }
    public string AccountNumber { get; set; }
    public string IBAN { get; set; }
    public string CardNumber { get; set; }
    public bool IsActive { get; set; }
    public int TenantId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string AccessToken { get; set; }
    public string Url { get; set; }
}