using Privatly.API.Domain.Entities.Entities.Payments;

namespace Privatly.API.ApplicationServices.Interfaces.Payment;

public interface ISubscriptionService
{
    Task<Subscription> CreateSubscriptionAsync(int userId, SubscriptionPlan subscriptionPlan,
        Transaction transaction);

    Task<bool> IsSubscriptionActiveAsync(int userId);

    Task<DateTime?> GetEndDateOfSubscriptionAsync(int userId);
}