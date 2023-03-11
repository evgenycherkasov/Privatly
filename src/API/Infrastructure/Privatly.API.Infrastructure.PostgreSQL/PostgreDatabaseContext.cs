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
            new SubscriptionPlan("Месяц", "Полный доступ на месяц к Privatly VPN",
                30, 600, false)
            {
                Id = 1
            },
            new SubscriptionPlan("Три месяца", "Полный доступ на 3 месяца к Privatly VPN",
                90, 1600, false)
            {
                Id = 2,
            },
            new SubscriptionPlan("Год", "Полный доступ на 12 месяцев к Privatly VPN", 
                365, 4200, false)
            {
                Id = 3,
            }
        );
    }
}