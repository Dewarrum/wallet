namespace Wallet.Domain.Transactions;

public sealed class Transaction(
    Guid id,
    Guid profileId,
    decimal amount,
    DateTime createdAt,
    string description,
    Guid categoryId,
    TransactionType type,
    bool isRecurring,
    PaymentMethod paymentMethod,
    string? merchant
)
{
    private Transaction()
        : this(
            default,
            default,
            default,
            default,
            default!,
            default,
            default,
            default,
            default,
            default
        ) { }

    public Guid Id { get; } = id;
    public Guid ProfileId { get; } = profileId;
    public decimal Amount { get; } = amount;
    public DateTime CreatedAt { get; } = createdAt;
    public string Description { get; } = description;
    public Guid CategoryId { get; } = categoryId;
    public TransactionType Type { get; } = type;
    public bool IsRecurring { get; } = isRecurring;
    public PaymentMethod PaymentMethod { get; } = paymentMethod;
    public string? Merchant { get; } = merchant;
}
