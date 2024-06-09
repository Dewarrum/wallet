using Microsoft.Extensions.DependencyInjection;
using Wallet.Application.Categories;
using Wallet.Application.Profiles;
using Wallet.Application.Transactions;
using Wallet.Application.Users;

namespace Wallet.Application;

public static class ApplicationModule
{
    public static void Configure(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<ICategoryService, CategoryService>();
    }
}
