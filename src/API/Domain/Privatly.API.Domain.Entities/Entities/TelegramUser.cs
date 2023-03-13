namespace Privatly.API.Domain.Entities.Entities;

public record TelegramUser : User
{
    public TelegramUser()
    {
        
    }
    
    public string TelegramId { get; set; }
    public string? UserName { get; set; }
}
