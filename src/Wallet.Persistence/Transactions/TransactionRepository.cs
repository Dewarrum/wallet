using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Transactions;

namespace Wallet.Persistence.Transactions;

internal sealed class TransactionRepository(WalletDbContext dbContext) : ITransactionRepository
{
    public async Task Add(Transaction transaction, CancellationToken cancellationToken = default)
    {
        await dbContext.Transactions.AddAsync(transaction, cancellationToken);
    }

    public async Task<Transaction?> Get(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Transaction>> GetForProfile(
        Guid profileId,
        int skip,
        int take,
        CancellationToken cancellationToken = default
    )
    {
        return await dbContext
            .Transactions.Where(t => t.ProfileId == profileId)
            .Skip(skip)
            .Take(take)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public void Remove(Transaction transaction)
    {
        dbContext.Transactions.Remove(transaction);
    }
}
