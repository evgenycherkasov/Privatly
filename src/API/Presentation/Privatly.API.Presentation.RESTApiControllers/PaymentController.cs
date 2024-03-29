﻿using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Privatly.API.ApplicationServices.Interfaces;
using Privatly.API.ApplicationServices.Interfaces.Payment;
using Privatly.API.Domain.Contracts;
using Privatly.API.Domain.Entities.Entities;
using Privatly.API.Domain.Entities.Entities.Payments;
using Privatly.API.Presentation.RESTApiControllers.Middlewares;
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
    private readonly IRabbitMqService _rabbitMqService;

    public PaymentController(IPaymentService paymentService, IUserService userService,
        ISubscriptionPlanService subscriptionPlanService, ITransactionService transactionService,
        ISubscriptionService subscriptionService, IRabbitMqService rabbitMqService)
    {
        _paymentService = paymentService;
        _userService = userService;
        _subscriptionPlanService = subscriptionPlanService;
        _transactionService = transactionService;
        _subscriptionService = subscriptionService;
        _rabbitMqService = rabbitMqService;
    }

    [HttpGet]
    [Route("create_payment/{userId}/{subscriptionPlanId}/{returnUrl}")]
    public async Task<string> CreatePayment(int userId, int subscriptionPlanId, string returnUrl)
    {
        var decodedReturnUrl = HttpUtility.UrlDecode(returnUrl);
        
        var user = await _userService.GetBy(userId);

        if (user is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return string.Empty;
        }

        var subscriptionPlan = await _subscriptionPlanService.GetSubscriptionPlan(subscriptionPlanId);

        if (subscriptionPlan is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return string.Empty;
        }

        var payment = await _paymentService.CreatePaymentAsync(userId, subscriptionPlan, decodedReturnUrl);

        await _transactionService.CreateTransaction(userId, payment.Id, TransactionStatus.Pending, payment.Price,
            DateTime.Now);

        return payment.PaymentUrl;
    }

    //[ServiceFilter(typeof(ClientIpCheckActionFilter))]
    [HttpPost]
    public async Task PaymentCallbackHandler()
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

            var subscription =  await _subscriptionService.CreateSubscriptionAsync(userId, subscriptionPlan, transaction);

            var telegramUser = (TelegramUser)(await _userService.GetBy(userId))!;

            foreach (var availableQueue in _rabbitMqService.AvailableQueues)
            {
                await _rabbitMqService.Post(
                    new SuccessPaymentDto(userId, telegramUser.TelegramId, telegramUser.Login, telegramUser.Password,
                        subscription.EndTime),
                    availableQueue);
            }
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