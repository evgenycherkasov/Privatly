namespace Privatly.API.Domain.Entities.Entities.Payments;

public enum TransactionStatus
{
    Pending,
    WaitingForCapture,
    Succeeded,
    Canceled
}