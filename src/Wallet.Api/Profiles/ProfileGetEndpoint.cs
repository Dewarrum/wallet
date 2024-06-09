using Wallet.Api.Users;
using Wallet.Application.Profiles;
using Wallet.Application.Users;
using Wallet.Domain.Profiles;
using Wallet.Domain.Users;

namespace Wallet.Api.Profiles;

public static class ProfileGetEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet(
            "{id:guid}",
            async (
                Guid id,
                IProfileService profileService,
                IUserContext userContext,
                CancellationToken cancellationToken
            ) =>
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

                Profile profile;
                try
                {
                    profile = await profileService.Get(id, cancellationToken);
                }
                catch (ProfileNotFoundException)
                {
                    return Results.NotFound();
                }

                if (profile.UserId != user.Id)
                {
                    return Results.NotFound();
                }

                return Results.Json(ProfileDto.From(profile));
            }
        );
    }
}
