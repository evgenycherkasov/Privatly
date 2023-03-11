using Privatly.API.Domain.Entities.Entities.Payments;

namespace Privatly.API.ApplicationServices.Interfaces.Payment;

public interface ITransactionService
{
    Task<List<Transaction?>> GetUserTransactionsAsync(int userId);

    Task<Transaction?> GetTransactionAsync(string transactionId);

    Task CreateTransaction(int userId, string transactionId, TransactionStatus status, decimal price, DateTime startTimeStamp);

    Task UpdateTransactionStatus(string transactionId, TransactionStatus status);
}