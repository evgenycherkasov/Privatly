using Privatly.API.Domain.Entities.Entities.Payments;

namespace Privatly.API.Domain.Interfaces;

public interface ISubscriptionPlanRepository : IGenericRepository<SubscriptionPlan>
{
    Task<SubscriptionPlan> AddAsync(string name, string description, int durationDays, decimal price);
}