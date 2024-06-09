using Wallet.Domain.Profiles;

namespace Wallet.Application.Profiles;

public interface IProfileService
{
    Task<Profile> Create(
        CreateProfileRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Profile> Get(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Profile>> GetForUser(
        Guid userId,
        CancellationToken cancellationToken = default
    );
}
