using Wallet.Api.Users;
using Wallet.Application.Profiles;
using Wallet.Application.Users;
using Wallet.Domain.Users;

namespace Wallet.Api.Profiles;

public static class ProfileCreateEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapPost(
            "",
            async (
                string? currency,
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

                var profile = await profileService.Create(
                    new CreateProfileRequest(user.Id, currency),
                    cancellationToken
                );

                return Results.Json(ProfileDto.From(profile));
            }
        );
    }
}