using Wallet.Domain.Transactions;

namespace Wallet.Application.Transactions;

public interface ITransactionService
{
    Task<Transaction> Create(
        CreateTransactionRequest request,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<Transaction>> GetForProfile(
        Guid profileId,
        int skip,
        int take,
        CancellationToken cancellationToken = default
    );

    Task<Transaction> Get(Guid id, CancellationToken cancellationToken = default);
}
