using Privatly.API.ApplicationServices.Interfaces;
using Privatly.API.Domain.Entities.Entities;
using Privatly.API.Domain.Interfaces;

namespace Privatly.API.ApplicationServices.Implementations;

public class TelegramUserService : ITelegramUserService
{
    private readonly ITelegramUserRepository _telegramUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TelegramUserService(ITelegramUserRepository telegramUserRepository, IUnitOfWork unitOfWork)
    {
        _telegramUserRepository = telegramUserRepository ?? throw new ArgumentNullException(nameof(telegramUserRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<TelegramUser> Create(string telegramId, string userName)
    {
        if (string.IsNullOrEmpty(telegramId))
            throw new ArgumentNullException(nameof(telegramId));

        var userPassword = Utils.Utils.GenerateRandomPassword();

        var user = await _telegramUserRepository.AddAsync(telegramId, userName, userPassword);
        
        await _unitOfWork.CommitAsync();

        return user;
    }

    public async Task<int?> GetUserIdBy(string telegramId)
    {
        if (string.IsNullOrEmpty(telegramId))
            throw new ArgumentNullException(nameof(telegramId));

        var telegramUsers = await _telegramUserRepository.GetAsync(u => u.TelegramId == telegramId);

        var user = telegramUsers.FirstOrDefault();

        return user?.Id;
    }
}