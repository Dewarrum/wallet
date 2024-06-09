using Wallet.Api.Utils;
using Wallet.Application.Profiles;

namespace Wallet.Api.Profiles;

public static class ProfileGetManyEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet(
            "",
            async (
                Guid userId,
                IProfileService profileService,
                CancellationToken cancellationToken
            ) =>
            {
                var profiles = await profileService.GetForUser(userId, cancellationToken);

                return Results.Json(
                    new PageResponse<ProfileDto>(profiles.Select(ProfileDto.From).ToList())
                );
            }
        );
    }
}
