using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Profiles;

namespace Wallet.Api.Profiles;

public static class ProfileCreateEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapPost(
            "",
            async (
                [FromBody] CreateProfileRequest request,
                IProfileService profileService,
                CancellationToken cancellationToken
            ) =>
            {
                var profile = await profileService.Create(request, cancellationToken);

                return Results.Json(ProfileDto.From(profile));
            }
        );
    }
}
