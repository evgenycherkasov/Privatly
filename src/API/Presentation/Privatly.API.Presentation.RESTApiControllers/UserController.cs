using Microsoft.AspNetCore.Mvc;
using Privatly.API.ApplicationServices.Interfaces;
using Privatly.API.ApplicationServices.Interfaces.Payment;
using Privatly.API.Domain.Contracts;

namespace Privatly.API.Presentation.RESTApiControllers;

[ApiController]
[Route("api/user/{userId:int}")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ISubscriptionService _subscriptionService;

    public UserController(IUserService userService, ISubscriptionService subscriptionService)
    {
        _userService = userService;
        _subscriptionService = subscriptionService;
    }

    [HttpPost("")]
    public async Task<IActionResult> UpdatePassword(int userId, string? oldPassword, string newPassword)
    {
        var user = await _userService.GetBy(userId);

        if (user is null)
            return NotFound();

        await _userService.SetPassword(user.Id, oldPassword, newPassword);

        return Ok();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetUserBy(int userId)
    {
        var user = await _userService.GetBy(userId);

        if (user is null)
            return NotFound();

        var subscriptionEndDate = await _subscriptionService.GetEndDateOfSubscriptionAsync(userId);

        var userDto = new UserDto(user.Id, subscriptionEndDate);
        
        return new OkObjectResult(userDto);
    }
}