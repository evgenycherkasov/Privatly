using Privatly.API.ApplicationServices.Interfaces;
using Privatly.API.Domain.Entities.Entities;
using Privatly.API.Domain.Interfaces;

namespace Privatly.API.ApplicationServices.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task SetPassword(int userId, string? oldPasswordHash, string newPasswordHash)
    {
        var user = await _userRepository.GetAsync(userId);
        
        if (user is null)
            return;

        if (user.Password == oldPasswordHash)
        {
            user.Password = newPasswordHash;
        }
    }

    public Task<User?> GetBy(int userId)
    {
        return _userRepository.GetAsync(userId);
    }

    public async Task<User?> GetBy(string login, string passwordHash)
    {
        var user = await _userRepository.GetAsync(u => u.Login == login && u.Password == passwordHash);

        return user.FirstOrDefault();
    }
}