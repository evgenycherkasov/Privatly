using Privatly.API.Domain.Entities.Entities.Payments;

namespace Privatly.API.ApplicationServices.Interfaces.Payment;

public interface ISubscriptionPlanService
{
    Task<SubscriptionPlan?> GetSubscriptionPlan(int id);
}