namespace Privatly.API.Domain.Entities.Entities.Payments;

public record SubscriptionPlan : Entity<int>
{
    public SubscriptionPlan() {}
    
    public string Name { get; set; }
    public string Description { get; set; }
    public int DurationDays { get; set; }
    public decimal Price { get; set; }
    public bool IsObsolete { get; set; }
}