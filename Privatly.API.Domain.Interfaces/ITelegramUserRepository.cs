using Privatly.API.Domain.Entities.Entities;

namespace Privatly.API.Domain.Interfaces;

public interface ITelegramUserRepository : IGenericRepository<TelegramUser>
{
    Task<TelegramUser> AddAsync(string telegramId, string? userName);
}