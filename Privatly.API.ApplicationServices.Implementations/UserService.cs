using Privatly.API.ApplicationServices.Interfaces;
using Privatly.API.Domain.Interfaces;

namespace Privatly.API.ApplicationServices.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task SetPassword(int userId, string oldPasswordHash, string newPasswordHash)
    {
        var user = await _userRepository.GetAsync(userId);
        
        if (user is null)
            return;

        if (user.Password is null || user.Password == oldPasswordHash)
        {
            user.Password = newPasswordHash;
        }
    }
}