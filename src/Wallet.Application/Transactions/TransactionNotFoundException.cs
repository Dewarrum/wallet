using Wallet.Application.Utils;
using Wallet.Domain.Transactions;

namespace Wallet.Application.Transactions;

public sealed class TransactionNotFoundException(Guid id)
    : ResourceNotFoundException(nameof(Transaction), id.ToString("N"));
