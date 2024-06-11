using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Users;

namespace Wallet.Api.Users;

public static class UserCreateEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapPost(
            "/",
            async (
                [FromBody] RegisterUserRequest request,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                try
                {
                    var user = await userService.Register(request, cancellationToken);
                    return Results.CreatedAtRoute("GetUser", new { id = user.Id }, user);
                }
                catch (UserAlreadyExistsException)
                {
                    return Results.Conflict();
                }
            }
        );
    }
}
