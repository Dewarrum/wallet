namespace Wallet.Persistence.UnitOfWork;

internal sealed class UnitOfWork(WalletDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
