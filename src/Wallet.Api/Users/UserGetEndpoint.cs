using Wallet.Application.Users;

namespace Wallet.Api.Users;

public static class UserGetEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet(
            "{id:guid}",
            async (
                Guid id,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var user = await userService.GetById(id, cancellationToken);
                return Results.Json(user);
            }
        );
    }
}