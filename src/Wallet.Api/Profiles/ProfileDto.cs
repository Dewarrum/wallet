using Wallet.Domain.Profiles;

namespace Wallet.Api.Profiles;

public sealed record ProfileDto(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    string Currency,
    DateTime CreatedAt
)
{
    public static ProfileDto From(Profile profile) =>
        new(
            profile.Id,
            profile.UserId,
            profile.Name,
            profile.Description,
            profile.Currency,
            profile.CreatedAt
        );
}
