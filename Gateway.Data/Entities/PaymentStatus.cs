namespace Gateway.Data.Entities;

public enum PaymentStatus : byte
{
    Request = 1,
    Verify = 2,
    Complete = 3
}