using Privatly.API.Domain.Entities.Entities;
using Privatly.API.Domain.Interfaces;
using Privatly.API.Infrastructure.Database;

namespace Privatly.API.Infrastructure.PostgreSQL.Repositories;

public class UserRepository : EFGenericRepository<User>, IUserRepository
{
    public UserRepository(PostgreDatabaseContext context) : base(context)
    {
    }
}