using Wallet.Domain.Transactions;

namespace Wallet.Application.Transactions;

public sealed record CreateTransactionRequest(
    Guid ProfileId,
    decimal Amount,
    string Description,
    Guid CategoryId,
    TransactionType TransactionType,
    bool IsRecurring,
    PaymentMethod PaymentMethod,
    string? Merchant
);
