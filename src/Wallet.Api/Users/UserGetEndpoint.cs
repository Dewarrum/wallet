using Wallet.Application.Users;

namespace Wallet.Api.Users;

public static class UserGetEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet(
            "",
            async (string email, IUserService userService, CancellationToken cancellationToken) =>
            {
                try
                {
                    var user = await userService.GetByEmail(email, cancellationToken);
                    return Results.Json(UserInfoDto.From(user));
                }
                catch (UserNotFoundException)
                {
                    return Results.NotFound();
                }
            }
        );
    }
}
