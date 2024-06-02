using Wallet.Domain.Profiles;

namespace Wallet.Persistence.Profiles;

public interface IProfileRepository
{
    Task Add(Profile profile, CancellationToken cancellationToken = default);

    Task<Profile?> Get(Guid profileId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Profile>> GetForUser(
        Guid userId,
        CancellationToken cancellationToken = default
    );

    void Remove(Profile profile);
}
