using Privatly.API.Domain.Entities.Entities;

namespace Privatly.API.ApplicationServices.Interfaces;

public interface IUserService
{
    Task<User?> GetBy(int userId);

    Task<User?> GetBy(string login, string passwordHash);
}