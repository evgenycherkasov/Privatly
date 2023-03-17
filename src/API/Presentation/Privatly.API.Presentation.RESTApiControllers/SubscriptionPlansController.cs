using Microsoft.AspNetCore.Mvc;
using Privatly.API.ApplicationServices.Interfaces.Payment;
using Privatly.API.Domain.Contracts;

namespace Privatly.API.Presentation.RESTApiControllers;

[ApiController]
[Route("/api/subscriptionPlans")]
public class SubscriptionPlansController : ControllerBase
{
    private readonly ISubscriptionPlanService _subscriptionPlanService;

    public SubscriptionPlansController(ISubscriptionPlanService subscriptionPlanService)
    {
        _subscriptionPlanService = subscriptionPlanService;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<SubscriptionPlanDto>> GetSubscriptionPlans()
    {
        var subscriptionPlans = await _subscriptionPlanService.GetSubscriptionPlans();

        return subscriptionPlans.Select(s => new SubscriptionPlanDto(s.Id, s.Name, s.Price, s.Description))
            .ToList();
    }
}