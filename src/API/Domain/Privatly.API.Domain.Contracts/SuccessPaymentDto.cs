namespace Privatly.API.Domain.Contracts;

public record SuccessPaymentDto(int UserId, string TelegramId, string Login, string Password, DateTime EndSubscriptionDateTime);