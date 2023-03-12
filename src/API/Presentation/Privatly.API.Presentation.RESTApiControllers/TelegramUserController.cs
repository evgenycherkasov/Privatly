using Microsoft.AspNetCore.Mvc;
using Privatly.API.ApplicationServices.Interfaces;
using Privatly.API.Domain.Contracts;

namespace Privatly.API.Presentation.RESTApiControllers;

[ApiController]
[Route("api/telegram_user/{telegramId}")]
public class TelegramUserController : ControllerBase
{
    private readonly ITelegramUserService _telegramUserService;

    public TelegramUserController(ITelegramUserService telegramUserService)
    {
        _telegramUserService = telegramUserService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(string telegramId, string? username)
    {
        var user = await _telegramUserService.Create(telegramId, username);

        var userDto = new UserDto(user.Id, null);

        return new OkObjectResult(userDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserIdBy(string telegramId)
    {
        var userId = await _telegramUserService.GetUserIdBy(telegramId);

        if (!userId.HasValue)
            return NotFound();

        return new OkObjectResult(userId.Value);
    }
}