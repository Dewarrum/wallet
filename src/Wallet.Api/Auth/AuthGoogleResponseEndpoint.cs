using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Wallet.Application.Users;

namespace Wallet.Api.Auth;

public static class AuthGoogleResponseEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet(
            "google-response",
            async (
                HttpContext httpContext,
                string backUrl,
                IUserService userService,
                CancellationToken cancellationToken
            ) =>
            {
                var result = await httpContext.AuthenticateAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme
                );
                if (!result.Succeeded)
                    return Results.Unauthorized();

                var email = result.Principal.FindFirstValue(ClaimTypes.Email);
                if (email is null)
                    return Results.Unauthorized();

                var name = result.Principal.FindFirstValue(ClaimTypes.Name);
                if (name is null)
                    return Results.Unauthorized();

                await EnsureUserIsRegistered(userService, name, email, cancellationToken);

                return Results.Redirect(backUrl);
            }
        );
    }

    private static async Task EnsureUserIsRegistered(
        IUserService userService,
        string name,
        string email,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var request = new RegisterUserRequest(name, email);
            await userService.Register(request, cancellationToken);
        }
        catch (UserAlreadyExistsException) { }
    }
}
