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
    public async Task<IActionResult> IsUserHasActiveSubscription(string login, string passwordHash)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(passwordHash))
            return NotFound();

        var user = await _userService.GetBy(login, passwordHash);

        if (user is null)
            return NotFound();

        var isUserHasActiveSubscription = await _subscriptionService.IsSubscriptionActiveAsync(user.Id);

        return Ok(isUserHasActiveSubscription);
    }
}