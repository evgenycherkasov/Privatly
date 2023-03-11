using Privatly.API.Domain.Entities.Entities.Payments;

namespace Privatly.API.ApplicationServices.Interfaces.Payment;

public interface IPaymentService
{
    Task<Domain.Models.Payment> CreatePaymentAsync(string userId, SubscriptionPlan subscriptionPlan, string returnUrl);

    Task CapturePayment(string transactionId);
}