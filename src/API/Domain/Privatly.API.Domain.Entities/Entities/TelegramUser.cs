namespace Privatly.API.Domain.Entities.Entities;

public record TelegramUser(string TelegramId, string? UserName) : User(TelegramId);
