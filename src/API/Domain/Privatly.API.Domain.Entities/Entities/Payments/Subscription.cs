namespace Privatly.API.Domain.Entities.Entities.Payments;

public record Subscription(int UserId, SubscriptionPlan SubscriptionPlan, Transaction Transaction,
    DateTime StartTime, DateTime EndTime) : Entity<int>;