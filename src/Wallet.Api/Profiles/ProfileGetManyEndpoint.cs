using Wallet.Api.Users;
using Wallet.Api.Utils;
using Wallet.Application.Profiles;
using Wallet.Application.Users;
using Wallet.Domain.Users;

namespace Wallet.Api.Profiles;

public static class ProfileGetManyEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet(
            "",
            async (
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

                var profiles = await profileService.GetForUser(user.Id, cancellationToken);

                return Results.Json(
                    new PageResponse<ProfileDto>(profiles.Select(ProfileDto.From).ToList())
                );
            }
        );
    }
}
