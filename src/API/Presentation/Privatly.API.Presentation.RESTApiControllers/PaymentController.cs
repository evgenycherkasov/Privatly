using Microsoft.AspNetCore.Mvc;
using Privatly.API.ApplicationServices.Interfaces;
using Privatly.API.ApplicationServices.Interfaces.Payment;
using Privatly.API.Domain.Entities.Entities.Payments;
using Privatly.API.Middlewares;
using Yandex.Checkout.V3;

namespace Privatly.API.Presentation.RESTApiControllers;

[ApiController]
[Route("api/payment")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly IUserService _userService;
    private readonly ISubscriptionPlanService _subscriptionPlanService;
    private readonly ITransactionService _transactionService;
    private readonly ISubscriptionService _subscriptionService;

    public PaymentController(IPaymentService paymentService, IUserService userService,
        ISubscriptionPlanService subscriptionPlanService,
        ITransactionService transactionService, ISubscriptionService subscriptionService)
    {
        _paymentService = paymentService;
        _userService = userService;
        _subscriptionPlanService = subscriptionPlanService;
        _transactionService = transactionService;
        _subscriptionService = subscriptionService;
    }

    [HttpGet("create_payment/{userId}/{subscriptionPlanId}/{returnUrl}")]
    public async Task<IActionResult> CreatePayment(int userId, int subscriptionPlanId, string returnUrl)
    {
        var user = await _userService.GetBy(userId);

        if (user is null)
            return new NotFoundResult();

        var subscriptionPlan = await _subscriptionPlanService.GetSubscriptionPlan(subscriptionPlanId);

        if (subscriptionPlan is null)
            return new NotFoundResult();

        var payment = await _paymentService.CreatePaymentAsync(userId, subscriptionPlan, returnUrl);

        await _transactionService.CreateTransaction(userId, payment.Id, TransactionStatus.Pending, payment.Price,
            DateTime.Now);
        return new OkObjectResult(payment.PaymentUrl);
    }

    [ServiceFilter(typeof(ClientIpCheckActionFilter))]
    [HttpPost]
    public async Task<IActionResult> PaymentCallbackHandler()
    {
        try
        {
            var message = await AsyncClient.ParseMessageAsync(Request.Method, Request.ContentType, Request.Body);
            var transactionId = message.Object.Id;
            var transactionStatus = message.Object.Status switch
            {
                PaymentStatus.Pending => TransactionStatus.Pending,
                PaymentStatus.Canceled => TransactionStatus.Canceled,
                PaymentStatus.WaitingForCapture => TransactionStatus.WaitingForCapture,
                PaymentStatus.Succeeded => TransactionStatus.Succeeded,
                _ => throw new ArgumentException("Неизвестный статус транзакции")
            };
            await _transactionService.UpdateTransactionStatus(transactionId, transactionStatus);

            if (transactionStatus == TransactionStatus.WaitingForCapture)
            {
                await HandleWaitingForCapturePayment(message);
            }
            else if (transactionStatus == TransactionStatus.Succeeded)
            {
                await HandleSucceededPaymentAsync(message, transactionId);
            }

            return Ok();
        }
        catch (ArgumentException)
        {
            //log
            return new NotFoundResult();
        }
    }

    private async Task HandleSucceededPaymentAsync(Message message, string transactionId)
    {
        if (message.Object.Metadata.TryGetValue("userId", out var userIdString)
            && message.Object.Metadata.TryGetValue("subscriptionPlanId", out var subscriptionPlanIdString))
        {
            var subscriptionPlanId = int.Parse(subscriptionPlanIdString);
            var userId = int.Parse(userIdString);

            var subscriptionPlan = await _subscriptionPlanService.GetSubscriptionPlan(subscriptionPlanId);
            
            if (subscriptionPlan is null)
                throw new ArgumentException();
            
            var transaction = await _transactionService.GetTransactionAsync(transactionId);

            if (transaction is null)
                throw new ArgumentException();

            await _subscriptionService.CreateSubscriptionAsync(userId, subscriptionPlan, transaction);
        }
        else
        {
            throw new InvalidOperationException($"Нет метаданных у транзакции {transactionId}");
        }
    }

    private async Task HandleWaitingForCapturePayment(Message message)
    {
        await _paymentService.CapturePayment(message.Object.Id);
    }
}