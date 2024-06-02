using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Users;

namespace Wallet.Persistence.Users;

internal sealed class UserRepository(WalletDbContext dbContext) : IUserRepository
{
    public async Task Add(User user, CancellationToken cancellationToken = default)
    {
        await dbContext.Users.AddAsync(user, cancellationToken);
    }

    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}
