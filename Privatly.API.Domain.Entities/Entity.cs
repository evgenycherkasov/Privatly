using System.ComponentModel.DataAnnotations;

namespace Privatly.API.Domain.Entities;

public abstract record BaseEntity();

public abstract record Entity<T> : BaseEntity
    where T : new()
{
    [Key]
    public T Id { get; set; }
}