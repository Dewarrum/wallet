namespace Wallet.Api.Users;

public static class UserEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("users").WithTags("users").RequireAuthorization();

        UserCreateEndpoint.Map(group);
        UserGetEndpoint.Map(group);
    }
}
