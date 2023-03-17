namespace Privatly.API.Domain.Entities.Entities;

public record User : Entity<int>
{
    public User()
    {
        
    }
    
    public string Password { get; set; }
    public string Login { get; set; }
}