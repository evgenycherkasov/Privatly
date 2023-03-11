namespace Privatly.API.Domain.Entities.Entities.Payments;

public record SubscriptionPlan(string Name, string Description, int DurationDays, decimal Price,
    bool IsObsolete) : Entity<int>;