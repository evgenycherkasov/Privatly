namespace Privatly.API.Domain.Entities.Entities.Payments;

public record Transaction : Entity<int>
{
    public Transaction()
    {
        
    }
    
    public TransactionStatus TransactionStatus { get; set; }
    public DateTime LastStatusUpdateTimeStamp { get; set; }
    public int UserId { get; set; }
    public string TransactionId { get; set; }
    public decimal Price { get; set; }
}