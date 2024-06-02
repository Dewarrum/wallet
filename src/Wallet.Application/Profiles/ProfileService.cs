using Wallet.Domain.Profiles;
using Wallet.Persistence.Profiles;
using Wallet.Persistence.UnitOfWork;

namespace Wallet.Application.Profiles;

internal sealed class ProfileService(IProfileRepository profileRepository, IUnitOfWork unitOfWork)
    : IProfileService
{
    public async Task<Profile> Create(
        CreateProfileRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var profile = new Profile(
            Guid.NewGuid(),
            request.UserId,
            request.Currency ?? "USD",
            DateTime.UtcNow
        );

        await profileRepository.Add(profile, cancellationToken);
        await unitOfWork.SaveChanges(cancellationToken);

        return profile;
    }

    public async Task<IReadOnlyList<Profile>> GetForUser(
        Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        return await profileRepository.GetForUser(userId, cancellationToken);
    }
}
