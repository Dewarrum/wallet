using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Wallet.Persistence;

public sealed class WalletDbContextFactory : IDesignTimeDbContextFactory<WalletDbContext>
{
    public WalletDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WalletDbContext>();
        optionsBuilder
            .UseNpgsql("Host=localhost;Database=wallet;Username=dev;Password=dev")
            .UseSnakeCaseNamingConvention();

        return new WalletDbContext(optionsBuilder.Options);
    }
}
