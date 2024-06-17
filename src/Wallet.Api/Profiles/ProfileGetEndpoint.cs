using Wallet.Application.Profiles;
using Wallet.Domain.Profiles;

namespace Wallet.Api.Profiles;

public static class ProfileGetEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet(
            "{id:guid}",
            async (Guid id, IProfileService profileService, CancellationToken cancellationToken) =>
            {
                Profile profile;
                try
                {
                    profile = await profileService.Get(id, cancellationToken);
                }
                catch (ProfileNotFoundException)
                {
                    return Results.NotFound();
                }

                return Results.Json(ProfileDto.From(profile));
            }
        );
    }
}
