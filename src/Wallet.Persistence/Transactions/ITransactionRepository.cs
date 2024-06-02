using Wallet.Domain.Transactions;

namespace Wallet.Persistence.Transactions;

public interface ITransactionRepository
{
    Task Add(Transaction transaction, CancellationToken cancellationToken = default);

    Task<Transaction?> Get(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Transaction>> GetForProfile(
        Guid profileId,
        CancellationToken cancellationToken = default
    );

    void Remove(Transaction transaction);
}
