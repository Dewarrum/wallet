using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Wallet.Application.Users;

namespace Wallet.Api.Accounts;

public static class AccountEndpoints
{
    public static IResult SignIn(
        HttpContext httpContext,
        ExternalAuthenticationProvider authenticationProvider,
        string? backUrl
    )
    {
        var redirectUri = authenticationProvider switch
        {
            ExternalAuthenticationProvider.Google
                => $"/api/account/signin-google?backUrl={backUrl ?? "/app"}",
            _ => null,
        };

        if (redirectUri is null)
            return Results.BadRequest("This authentication provider is not supported");

        var props = new AuthenticationProperties { RedirectUri = redirectUri };
        return Results.Challenge(props, [GoogleDefaults.AuthenticationScheme]);
    }

    public static IResult SignUp(ExternalAuthenticationProvider authenticationProvider)
    {
        var redirectUri = authenticationProvider switch
        {
            ExternalAuthenticationProvider.Google => "/api/account/signup-google",
            _ => null,
        };

        if (redirectUri is null)
            return Results.BadRequest("This authentication provider is not supported");

        var props = new AuthenticationProperties { RedirectUri = redirectUri };
        return Results.Challenge(props, [GoogleDefaults.AuthenticationScheme]);
    }

    public static async Task<IResult> GoogleSignIn(
        HttpContext httpContext,
        IUserService userService,
        string? backUrl,
        CancellationToken cancellationToken = default
    )
    {
        var response = await httpContext.AuthenticateAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        if (response.Principal == null)
            return Results.BadRequest();

        var email = response.Principal.FindFirstValue(ClaimTypes.Email);
        if (email is null)
            return Results.Problem();

        try
        {
            var user = await userService.GetByEmail(email, cancellationToken);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString("N")),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.Name),
            };
            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );
            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            return Results.Redirect(backUrl ?? "/app");
        }
        catch (UserNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
    }

    public static async Task<IResult> GoogleSignUp(
        HttpContext httpContext,
        IUserService userService,
        CancellationToken cancellationToken
    )
    {
        var response = await httpContext.AuthenticateAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        if (response.Principal == null)
            return Results.Forbid();

        var name = response.Principal.FindFirstValue(ClaimTypes.Name);
        if (name is null)
            return Results.Problem();

        var email = response.Principal.FindFirstValue(ClaimTypes.Email);
        if (email is null)
            return Results.Problem();

        try
        {
            var request = new RegisterUserRequest(name, email);
            var user = await userService.Register(request, cancellationToken);
            return Results.Json(AccountDto.From(user));
        }
        catch (UserAlreadyExistsException e)
        {
            return Results.Conflict(e.Message);
        }
    }

    public static async Task<IResult> MyInfo(
        HttpContext httpContext,
        CancellationToken cancellationToken
    )
    {
        var response = await httpContext.AuthenticateAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        if (response.Principal is not { } claimsPrincipal)
            return Results.Forbid();

        var account = new AccountDto(
            Guid.Parse(
                claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value
            ),
            claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.Name).Value,
            claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.Email).Value
        );

        return Results.Json(account);
    }
}
