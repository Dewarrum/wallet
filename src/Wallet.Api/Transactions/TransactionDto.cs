using Wallet.Domain.Transactions;

namespace Wallet.Api.Transactions;

public sealed record TransactionDto(
    Guid Id,
    Guid ProfileId,
    decimal Amount,
    DateTime CreatedAt,
    string Description,
    Guid CategoryId,
    TransactionType Type,
    bool IsRecurring,
    PaymentMethod PaymentMethod,
    string? Merchant
)
{
    public static TransactionDto From(Transaction transaction) =>
        new(
            transaction.Id,
            transaction.ProfileId,
            transaction.Amount,
            transaction.CreatedAt,
            transaction.Description,
            transaction.CategoryId,
            transaction.Type,
            transaction.IsRecurring,
            transaction.PaymentMethod,
            transaction.Merchant
        );
}
