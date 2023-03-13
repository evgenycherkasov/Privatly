using Privatly.API.Domain.Entities.Entities.Payments;
using Privatly.API.Domain.Interfaces;
using Privatly.API.Infrastructure.Database;

namespace Privatly.API.Infrastructure.PostgreSQL.Repositories;

public class SubscriptionPlanRepository : EFGenericRepository<SubscriptionPlan>, ISubscriptionPlanRepository
{
    public SubscriptionPlanRepository(PostgreDatabaseContext context) : base(context)
    {
            
    }

    public async Task<SubscriptionPlan> AddAsync(string name, string description, int durationDays, decimal price)
    {
        var subscription = Create();
        subscription.Name = name;
        subscription.Description = description;
        subscription.DurationDays = durationDays;
        subscription.Price = price;
        return await AddAsync(subscription);
    }
}