namespace Privatly.API.Domain.Entities.Entities.Payments;

public record Subscription : Entity<int>
{
    public Subscription()
    {
    }

    public int UserId { get; set; }
    public SubscriptionPlan SubscriptionPlan { get; set; }
    public Transaction Transaction { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}