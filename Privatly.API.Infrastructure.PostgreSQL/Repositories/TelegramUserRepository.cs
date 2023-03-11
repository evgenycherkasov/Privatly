using Microsoft.EntityFrameworkCore;
using Privatly.API.Domain.Entities.Entities;
using Privatly.API.Domain.Interfaces;
using Privatly.API.Infrastructure.Database;

namespace Privatly.API.Infrastructure.PostgreSQL.Repositories;

public class TelegramUserRepository : EFGenericRepository<TelegramUser>, ITelegramUserRepository
{
    public TelegramUserRepository(DbContext context) : base(context)
    {
    }

    public Task<TelegramUser> AddAsync(string telegramId, string? userName)
    {
        var telegramUser = new TelegramUser(telegramId, userName)
        {
            Login = telegramId
        };

        return AddAsync(telegramUser);
    }
}