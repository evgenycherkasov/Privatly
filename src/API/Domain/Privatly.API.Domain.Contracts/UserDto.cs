namespace Privatly.API.Domain.Contracts;

public record UserDto(int Id, string Login, string Password, DateTime? SubscriptionEndDate);