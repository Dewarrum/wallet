using Wallet.Domain.Profiles;

namespace Wallet.Api.Profiles;

public sealed record ProfileDto(Guid Id, Guid UserId, string Currency, DateTime CreatedAt)
{
    public static ProfileDto From(Profile profile) =>
        new(profile.Id, profile.UserId, profile.Currency, profile.CreatedAt);
}
