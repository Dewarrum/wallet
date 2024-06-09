namespace Wallet.Api.Auth;

public static class AuthEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("auth").WithTags("auth");

        AuthLoginEndpoint.Map(group);
        AuthGoogleResponseEndpoint.Map(group);
        AuthProfileEndpoint.Map(group);
    }
}
