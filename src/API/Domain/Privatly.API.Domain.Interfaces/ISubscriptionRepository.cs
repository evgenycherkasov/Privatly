using Privatly.API.Domain.Entities.Entities.Payments;

namespace Privatly.API.Domain.Interfaces;

public interface ISubscriptionRepository : IGenericRepository<Subscription>
{
    Task<Subscription> AddAsync(int userId, SubscriptionPlan subscriptionPlan, Transaction transaction,
        DateTime startTime, DateTime endTime);

    /// <summary>
    /// Возвращает список подписок у которых дата окончание позже чем <paramref name="dateToCheck"/>
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="dateToCheck"></param>
    /// <returns></returns>
    Task<IEnumerable<Subscription?>> GetActiveSubscriptionsAsync(int userId, DateTime dateToCheck);
}