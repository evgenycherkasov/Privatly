using Privatly.API.Domain.Entities.Entities.Payments;
using Privatly.API.Domain.Interfaces;
using Privatly.API.Infrastructure.Database;

namespace Privatly.API.Infrastructure.PostgreSQL.Repositories;

public class TransactionRepository : EFGenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(PostgreDatabaseContext context) : base(context)
    {

    }

    public async Task<Transaction> AddAsync(int userId, string transactionId, TransactionStatus status,
        decimal price, DateTime lastStatusUpdateTimeStamp)
    {
        var transaction = Create();
        transaction.TransactionId = transactionId;
        transaction.TransactionStatus = status;
        transaction.LastStatusUpdateTimeStamp = lastStatusUpdateTimeStamp;
        transaction.UserId = userId;
        transaction.Price = price;

        return await AddAsync(transaction);
    }

    public async Task UpdateTransactionStatus(string transactionId, TransactionStatus status)
    {
        var transactions = await GetAsync(t => t.TransactionId == transactionId, take: 1);
        var transaction = transactions.FirstOrDefault();
        
        if (transaction is null)
            return;
        
        transaction.TransactionStatus = status;
        transaction.LastStatusUpdateTimeStamp = DateTime.Now;

        Update(transaction);
    }

    public Task<IEnumerable<Transaction?>> GetUserTransactionsAsync(int userId)
    {
        return GetAsync(transaction => transaction.UserId == userId);
    }

    public async Task<Transaction?> GetAsync(string transactionId)
    {
        return (await GetAsync(transaction => transaction.TransactionId == transactionId)).Single();
    }
}