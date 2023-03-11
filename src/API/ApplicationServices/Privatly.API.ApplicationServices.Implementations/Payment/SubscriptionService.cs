using Privatly.API.ApplicationServices.Interfaces.Payment;
using Privatly.API.Domain.Entities.Entities.Payments;
using Privatly.API.Domain.Interfaces;

namespace Privatly.API.ApplicationServices.Implementations.Payment;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Subscription> CreateSubscriptionAsync(int userId, SubscriptionPlan subscriptionPlan, Transaction transaction)
    {
        var activeSubscriptions = (await _subscriptionRepository.GetActiveSubscriptionsAsync(userId, DateTime.Now)).ToList();
        var startTime = activeSubscriptions.Any() 
            ? activeSubscriptions.Max(s => s!.EndTime) 
            : DateTime.Now;
        var endTime = startTime.AddDays(subscriptionPlan.DurationDays);
        var subscription = await _subscriptionRepository.AddAsync(userId, subscriptionPlan, transaction, startTime, endTime);
        await _unitOfWork.CommitAsync();

        return subscription;
    }

    public async Task<bool> IsSubscriptionActiveAsync(int userId)
    {
        var activeSubscriptions = await _subscriptionRepository.GetActiveSubscriptionsAsync(userId, DateTime.Now);
        return activeSubscriptions.Any();
    }

    public async Task<DateTime?> GetEndDateOfSubscriptionAsync(int userId)
    {
        var activeSubscriptions = await _subscriptionRepository.GetActiveSubscriptionsAsync(userId, DateTime.Now);
        var subscriptions = activeSubscriptions.ToList();
        return subscriptions.Any() 
            ? subscriptions.Max(s => s!.EndTime) 
            : null;
    }
}