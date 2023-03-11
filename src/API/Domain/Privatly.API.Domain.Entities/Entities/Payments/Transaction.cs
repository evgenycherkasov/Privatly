namespace Privatly.API.Domain.Entities.Entities.Payments;

public record Transaction(int UserId, string TransactionId, TransactionStatus TransactionStatus,
    DateTime LastStatusUpdateTimeStamp, decimal Price) : Entity<int>
{
    public TransactionStatus TransactionStatus { get; set; } = TransactionStatus;
    public DateTime LastStatusUpdateTimeStamp { get; set; } = LastStatusUpdateTimeStamp;
}