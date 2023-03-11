using Privatly.API.Domain.Entities.Entities.Payments;

namespace Privatly.API.Domain.Interfaces;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<Transaction> AddAsync(int userId,
        string transactionId, TransactionStatus status,
        decimal price, DateTime lastStatusUpdateTimeStamp);

    Task UpdateTransactionStatus(string transactionId, TransactionStatus status);

    Task<IEnumerable<Transaction?>> GetUserTransactionsAsync(int userId);

    Task<Transaction?> GetAsync(string transactionId);
}