using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Privatly.API.ApplicationServices.Interfaces;
using Privatly.API.ApplicationServices.Interfaces.Payment;
// ReSharper disable InconsistentNaming

namespace Privatly.API.Presentation.RESTApiControllers;

[ApiController]
[Route("api/openvpn")]
public class OpenVPNController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly IUserService _userService;

    public OpenVPNController(ISubscriptionService subscriptionService, IUserService userService)
    {
        _subscriptionService = subscriptionService;
        _userService = userService;
    }

    [HttpGet("{login}/{passwordHash}")]
    public async Task<bool?> IsUserHasActiveSubscription(string login, string passwordHash)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(passwordHash))
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return null;
        }

        var user = await _userService.GetBy(login, passwordHash);

        if (user is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return null;
        }
            
        var isUserHasActiveSubscription = await _subscriptionService.IsSubscriptionActiveAsync(user.Id);

        return isUserHasActiveSubscription;
    }
}