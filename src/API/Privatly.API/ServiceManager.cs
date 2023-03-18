using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Privatly.API.ApplicationServices.Implementations;
using Privatly.API.ApplicationServices.Implementations.Payment;
using Privatly.API.ApplicationServices.Interfaces;
using Privatly.API.ApplicationServices.Interfaces.Payment;
using Privatly.API.Domain.Interfaces;
using Privatly.API.Infrastructure.PostgreSQL;
using Privatly.API.Infrastructure.PostgreSQL.Repositories;
using Privatly.API.Infrastructure.RabbitMQ;
using Privatly.API.Infrastructure.Yookassa;
using Privatly.API.Presentation.RESTApiControllers;
using Privatly.API.Presentation.RESTApiControllers.Middlewares;

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
            options.UseNpgsql("host=127.0.0.1;port=5432;database=privatlyapi;username=admin;password=password"));// Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")));

        services.Configure<YookassaAuthData>(_configuration.GetSection("YookassaAuthData"));

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

        services.AddSingleton<IRabbitMqService, RabbitMqService>(_ =>
        {
            var rabbitMqHostName = _configuration.GetValue<string>("RabbitMqHostName");

            if (string.IsNullOrEmpty(rabbitMqHostName))
                throw new ArgumentException(nameof(rabbitMqHostName));

            return new RabbitMqService(rabbitMqHostName);
        });
        
        services.AddSingleton(container =>
        {
            var loggerFactory = container.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<ClientIpCheckActionFilter>();

            return new ClientIpCheckActionFilter(
                _configuration.GetValue<string>("YookassaIpsSafeList"), logger);
        });

        var controllersAssembly = typeof(PaymentController).GetTypeInfo().Assembly;
        
        services.AddControllers()
            .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(new AssemblyPart(controllersAssembly)));
        
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen();
    }
}