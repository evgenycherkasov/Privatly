using Privatly.API.ApplicationServices.Interfaces.Payment;
using Privatly.API.Domain.Entities.Entities.Payments;
using Privatly.API.Domain.Interfaces;

namespace Privatly.API.ApplicationServices.Implementations.Payment;

public class SubscriptionPlanService : ISubscriptionPlanService
{
    private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;

    public SubscriptionPlanService(ISubscriptionPlanRepository subscriptionPlanRepository)
    {
        _subscriptionPlanRepository = subscriptionPlanRepository ??
                                      throw new ArgumentNullException(nameof(subscriptionPlanRepository));
    }

    public Task<SubscriptionPlan?> GetSubscriptionPlan(int id)
    {
        return _subscriptionPlanRepository.GetAsync(id);
    }
}