using Microsoft.EntityFrameworkCore;
using Privatly.API.Domain.Entities.Entities;
using Privatly.API.Domain.Entities.Entities.Payments;

namespace Privatly.API.Infrastructure.PostgreSQL;

public sealed class PostgreDatabaseContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<TelegramUser>? TelegramUsers { get; set; }
    public DbSet<Transaction>? Transactions { get; set; }
    public DbSet<SubscriptionPlan>? SubscriptionPlans { get; set; }
    public DbSet<Subscription>? Subscriptions { get; set; }

    public PostgreDatabaseContext(DbContextOptions<PostgreDatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<SubscriptionPlan>().HasData(
            new SubscriptionPlan
            {
                Id = 1,
                Name = "Месяц",
                Description = "Полный доступ на месяц к Privatly VPN",
                DurationDays = 30,
                Price = 500,
                IsObsolete = false
            },
            new SubscriptionPlan
            {
                Id = 2,
                Name = "Три месяца",
                Description = "Полный доступ на 3 месяца к Privatly VPN",
                DurationDays = 90,
                Price = 1300,
                IsObsolete = false
            },
            new SubscriptionPlan
            {
                Id = 3,
                Name = "Год",
                Description = "Полный доступ на 12 месяцев к Privatly VPN",
                DurationDays = 365,
                Price = 4500,
                IsObsolete = false
            });
    }
}