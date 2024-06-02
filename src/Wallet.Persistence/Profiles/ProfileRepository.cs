using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Profiles;

namespace Wallet.Persistence.Profiles;

internal sealed class ProfileRepository(WalletDbContext dbContext) : IProfileRepository
{
    public async Task Add(Profile profile, CancellationToken cancellationToken = default)
    {
        await dbContext.Profiles.AddAsync(profile, cancellationToken);
    }

    public async Task<Profile?> Get(Guid profileId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Profiles.FirstOrDefaultAsync(
            p => p.Id == profileId,
            cancellationToken
        );
    }

    public async Task<IReadOnlyList<Profile>> GetForUser(
        Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        return await dbContext
            .Profiles.Where(p => p.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public void Remove(Profile profile)
    {
        dbContext.Remove(profile);
    }
}
