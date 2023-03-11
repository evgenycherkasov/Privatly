using System.Linq.Expressions;
using Privatly.API.Domain.Entities.Entities.Payments;
using Privatly.API.Domain.Interfaces;
using Privatly.API.Infrastructure.Database;

namespace Privatly.API.Infrastructure.PostgreSQL.Repositories;

public class SubscriptionRepository : EFGenericRepository<Subscription>, ISubscriptionRepository
{
    public SubscriptionRepository(PostgreDatabaseContext context) : base(context)
    {
    }

    public Task<Subscription> AddAsync(int userId, SubscriptionPlan subscriptionPlan, Transaction transaction,
        DateTime startTime, DateTime endTime)
    {
        var subscription = new Subscription(userId, subscriptionPlan, transaction, startTime, endTime);

        return AddAsync(subscription);
    }

    public Task<IEnumerable<Subscription?>> GetActiveSubscriptionsAsync(int userId, DateTime dateToCheck)
    {
        return GetAsync(s => s.UserId == userId && s.EndTime >= dateToCheck,
            subs => subs.OrderBy(s => s.StartTime),
            new Expression<Func<Subscription, object>>[]
            {
                s => s.Transaction,
                s => s.SubscriptionPlan
            });
    }
}