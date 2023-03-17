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
                Name = "1 Month",
                Description = "Get full access to Privately VPN for a month",
                DurationDays = 30,
                Price = 350,
                IsObsolete = false
            },
            new SubscriptionPlan
            {
                Id = 2,
                Name = "3 Months",
                Description = "Get full access to Privately VPN for 3 months",
                DurationDays = 90,
                Price = 800,
                IsObsolete = false
            },
            new SubscriptionPlan
            {
                Id = 3,
                Name = "1 Year",
                Description = "Get full access to Privately VPN for a year",
                DurationDays = 365,
                Price = 3500,
                IsObsolete = false
            });
    }
}