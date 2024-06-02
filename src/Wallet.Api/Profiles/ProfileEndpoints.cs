using Wallet.Api.Users;
using Wallet.Api.Utils;
using Wallet.Application.Profiles;
using Wallet.Application.Users;
using Wallet.Domain.Users;

namespace Wallet.Api.Profiles;

public static class ProfileEndpoints
{
    public static async Task<IResult> Create(
        string? currency,
        IProfileService profileService,
        IUserContext userContext,
        CancellationToken cancellationToken
    )
    {
        User user;
        try
        {
            user = await userContext.Get(cancellationToken);
        }
        catch (UserNotFoundException)
        {
            return Results.Forbid();
        }

        var profile = await profileService.Create(
            new CreateProfileRequest(user.Id, currency),
            cancellationToken
        );

        return Results.Json(ProfileDto.From(profile));
    }

    public static async Task<IResult> GetForUser(
        IProfileService profileService,
        IUserContext userContext,
        CancellationToken cancellationToken
    )
    {
        User user;
        try
        {
            user = await userContext.Get(cancellationToken);
        }
        catch (UserNotFoundException)
        {
            return Results.Forbid();
        }

        var profiles = await profileService.GetForUser(user.Id, cancellationToken);

        return Results.Json(
            new PageResponse<ProfileDto>(profiles.Select(ProfileDto.From).ToList())
        );
    }
}
