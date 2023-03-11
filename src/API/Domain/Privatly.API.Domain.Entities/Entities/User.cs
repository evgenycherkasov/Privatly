namespace Privatly.API.Domain.Entities.Entities;

public abstract record User(string Login) : Entity<int>
{
    public string? Password { get; set; }
}