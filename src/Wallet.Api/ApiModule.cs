using Wallet.Api.Users;

namespace Wallet.Api;

public static class ApiModule
{
    public static void Configure(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
    }
}
