using Privatly.API.ApplicationServices.Interfaces;
using Privatly.API.Domain.Entities.Entities;
using Privatly.API.Domain.Interfaces;

namespace Privatly.API.ApplicationServices.Implementations;

public class TelegramUserService : ITelegramUserService
{
    private readonly ITelegramUserRepository _telegramUserRepository;

    public TelegramUserService(ITelegramUserRepository telegramUserRepository)
    {
        _telegramUserRepository = telegramUserRepository ?? throw new ArgumentNullException(nameof(telegramUserRepository));
    }

    public Task<TelegramUser> Create(string telegramId, string? userName)
    {
        if (string.IsNullOrEmpty(telegramId))
            throw new ArgumentNullException(nameof(telegramId));

        return _telegramUserRepository.AddAsync(telegramId, userName);
    }
}