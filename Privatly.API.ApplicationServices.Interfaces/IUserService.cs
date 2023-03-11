namespace Privatly.API.ApplicationServices.Interfaces;

public interface IUserService
{
    Task SetPassword(int userId, string oldPasswordHash, string newPasswordHash);
}