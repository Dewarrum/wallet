namespace Wallet.Api.Profiles;

public static class ProfileEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("profiles").WithTags("profiles").RequireAuthorization();

        ProfileCreateEndpoint.Map(group);
        ProfileGetEndpoint.Map(group);
        ProfileGetManyEndpoint.Map(group);
    }
}
