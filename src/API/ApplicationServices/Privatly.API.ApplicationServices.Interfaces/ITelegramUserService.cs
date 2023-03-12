using Privatly.API.Domain.Entities.Entities;

namespace Privatly.API.ApplicationServices.Interfaces;

public interface ITelegramUserService
{
    Task<TelegramUser> Create(string telegramId, string? userName);

    Task<int?> GetUserIdBy(string telegramId);
}