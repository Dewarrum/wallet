using System.Security.Claims;
using Wallet.Application.Users;

namespace Wallet.Api.Auth;

public static class AuthProfileEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.Map(
            "profile",
            async (
                HttpContext httpContext,
                IUserService userService,
                CancellationToken cancellationToken = default
            ) =>
            {
                if (!httpContext.User.Identity?.IsAuthenticated is true)
                    return Results.Unauthorized();

                var email = httpContext.User.FindFirstValue(ClaimTypes.Email);
                if (email is null)
                    return Results.Unauthorized();

                try
                {
                    var user = await userService.GetByEmail(email, cancellationToken);
                    return Results.Ok(UserInfoDto.From(user));
                }
                catch (UserNotFoundException)
                {
                    return Results.Unauthorized();
                }
            }
        );
    }
}