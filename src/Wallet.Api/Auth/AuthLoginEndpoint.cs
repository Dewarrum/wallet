using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;

namespace Wallet.Api.Auth;

public static class AuthLoginEndpoint
{
    private const string DefaultBackUrl = "/app";

    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet(
            "login",
            (string? backUrl) =>
                Results.Challenge(
                    new AuthenticationProperties
                    {
                        RedirectUri =
                            $"/api/auth/google-response?backUrl={backUrl ?? DefaultBackUrl}",
                    },
                    [GoogleDefaults.AuthenticationScheme]
                )
        );
    }
}
