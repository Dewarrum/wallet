using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Persistence.Categories;
using Wallet.Persistence.Profiles;
using Wallet.Persistence.Transactions;
using Wallet.Persistence.UnitOfWork;
using Wallet.Persistence.Users;

namespace Wallet.Persistence;

public static class PersistenceModule
{
    public static void Configure(IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddDbContext<WalletDbContext>(options =>
        {
            options
                .UseNpgsql(configuration.GetConnectionString("PostgreSQL"))
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}
