using Microsoft.EntityFrameworkCore;
using Privatly.API.ApplicationServices.Implementations;
using Privatly.API.ApplicationServices.Implementations.Payment;
using Privatly.API.ApplicationServices.Interfaces;
using Privatly.API.ApplicationServices.Interfaces.Payment;
using Privatly.API.Domain.Interfaces;
using Privatly.API.Infrastructure.PostgreSQL;
using Privatly.API.Infrastructure.PostgreSQL.Repositories;
using Privatly.API.Infrastructure.Yookassa;

namespace Privatly.API;

public class ServiceManager
{
    private readonly IConfiguration _configuration;
    
    public ServiceManager(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException();
    }
    
    public void Configure(IServiceCollection services)
    {
        services.AddDbContext<PostgreDatabaseContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("ApiPostgresConnection")));

        services.Configure<YookassaAuthData>(_configuration.GetSection("YookassaAuthData")); //get from proc env;

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITelegramUserRepository, TelegramUserRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        services.AddScoped<IPaymentService, PaymentService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITelegramUserService, TelegramUserService>();
        services.AddScoped<ISubscriptionPlanService, SubscriptionPlanService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<ITransactionService, TransactionService>();
        
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}