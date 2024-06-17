using Wallet.Domain.Transactions;
using Wallet.Persistence.Transactions;
using Wallet.Persistence.UnitOfWork;

namespace Wallet.Application.Transactions;

internal sealed class TransactionService(
    ITransactionRepository transactionRepository,
    IUnitOfWork unitOfWork
) : ITransactionService
{
    public async Task<Transaction> Create(
        CreateTransactionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var transaction = new Transaction(
            Guid.NewGuid(),
            request.ProfileId,
            request.Amount,
            DateTime.UtcNow,
            request.Description,
            request.CategoryId,
            request.TransactionType,
            request.IsRecurring,
            request.PaymentMethod,
            request.Merchant
        );

        await transactionRepository.Add(transaction, cancellationToken);
        await unitOfWork.SaveChanges(cancellationToken);

        return transaction;
    }

    public async Task<IReadOnlyList<Transaction>> GetForProfile(
        Guid profileId,
        int skip,
        int take,
        CancellationToken cancellationToken = default
    )
    {
        return await transactionRepository.GetForProfile(profileId, skip, take, cancellationToken);
    }

    public async Task<Transaction> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var transaction = await transactionRepository.Get(id, cancellationToken);
        if (transaction is null)
            throw new TransactionNotFoundException(id);

        return transaction;
    }
}
