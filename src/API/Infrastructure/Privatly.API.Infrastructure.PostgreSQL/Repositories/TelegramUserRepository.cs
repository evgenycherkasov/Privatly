using Microsoft.EntityFrameworkCore;
using Privatly.API.Domain.Entities.Entities;
using Privatly.API.Domain.Interfaces;
using Privatly.API.Infrastructure.Database;

namespace Privatly.API.Infrastructure.PostgreSQL.Repositories;

public class TelegramUserRepository : EFGenericRepository<TelegramUser>, ITelegramUserRepository
{
    public TelegramUserRepository(PostgreDatabaseContext context) : base(context)
    {
    }

    public Task<TelegramUser> AddAsync(string telegramId, string? userName)
    {
        var telegramUser = Create();

        telegramUser.Login = telegramId;
        telegramUser.TelegramId = telegramId;
        telegramUser.UserName = userName;

        return AddAsync(telegramUser);
    }
}