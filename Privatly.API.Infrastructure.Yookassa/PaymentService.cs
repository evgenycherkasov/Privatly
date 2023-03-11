using Microsoft.Extensions.Options;
using Privatly.API.ApplicationServices.Interfaces.Payment;
using Privatly.API.Domain.Entities.Entities.Payments;
using Yandex.Checkout.V3;

namespace Privatly.API.Infrastructure.Yookassa;

public class PaymentService : IPaymentService
    {
        private readonly AsyncClient _paymentClient;

        public PaymentService(IOptions<YookassaAuthData> yookassaAuthData)
        {
            _paymentClient = new Client(yookassaAuthData.Value.ShopId, yookassaAuthData.Value.SecretKey).MakeAsync();
        }

        public async Task<Domain.Models.Payment> CreatePaymentAsync(string userId, SubscriptionPlan subscriptionPlan, string returnUrl)
        {
            var newPayment = new NewPayment()
            {
                Amount = new()
                {
                    Value = subscriptionPlan.Price,
                    Currency = "RUB"
                },
                Description = subscriptionPlan.Description,
                Confirmation = new()
                {
                    Type = ConfirmationType.Redirect,
                    ReturnUrl = returnUrl
                },
                Metadata = new()
                {
                    {"userId", userId},
                    {"subscriptionPlanId", subscriptionPlan.Id.ToString()}
                },
                Receipt = new()
                {
                    Items = new()
                    {
                        new()
                        {
                            Description = subscriptionPlan.Description,
                            Quantity = 1,
                            Amount = new()
                            {
                                Value = subscriptionPlan.Price,
                                Currency = "RUB"
                            },
                            VatCode = VatCode.Vat10,
                            PaymentSubject = PaymentSubject.Service,
                            PaymentMode = PaymentMode.FullPayment,
                        }
                    },
                }
            };

            var payment = await _paymentClient.CreatePaymentAsync(newPayment);
            return new(payment.Id, payment.Confirmation.ConfirmationUrl, payment.Amount.Value);
        }

        public async Task CapturePayment(string transactionId)
        {
            await _paymentClient.CapturePaymentAsync(transactionId);
        }
    }