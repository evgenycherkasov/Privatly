using Privatly.API.ApplicationServices.Interfaces.Payment;
using Privatly.API.Domain.Entities.Entities.Payments;
using Privatly.API.Domain.Interfaces;

namespace Privatly.API.ApplicationServices.Implementations.Payment;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TransactionService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
    {
        _transactionRepository =
            transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<List<Transaction?>> GetUserTransactionsAsync(int userId)
    {
        var transactions = await _transactionRepository.GetUserTransactionsAsync(userId);
        return transactions.ToList();
    }

    public Task<Transaction?> GetTransactionAsync(string transactionId)
    {
        return _transactionRepository.GetAsync(transactionId);
    }

    public async Task CreateTransaction(int userId, string transactionId, TransactionStatus status,
        decimal price, DateTime startTimeStamp)
    {
        await _transactionRepository.AddAsync(userId, transactionId, status, price, startTimeStamp);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateTransactionStatus(string transactionId, TransactionStatus status)
    {
        await _transactionRepository.UpdateTransactionStatus(transactionId, status);
        await _unitOfWork.CommitAsync();
    }
}