using Microsoft.AspNetCore.Http;
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

    [HttpPost("/create")]
    public async Task<UserDto> CreateUser(string telegramId, string? username)
    {
        var user = await _telegramUserService.Create(telegramId, username);

        var userDto = new UserDto(user.Id, null);

        return userDto;
    }

    [HttpGet]
    public async Task<int?> GetUserIdBy(string telegramId)
    {
        var userId = await _telegramUserService.GetUserIdBy(telegramId);

        if (userId.HasValue) 
            return userId.Value;
        
        Response.StatusCode = StatusCodes.Status404NotFound;
        return null;
    }
}