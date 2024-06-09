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

    public Guid Id { get; init; } = id;
    public Guid ProfileId { get; init; } = profileId;
    public decimal Amount { get; private set; } = amount;
    public DateTime CreatedAt { get; init; } = createdAt;
    public string Description { get; private set; } = description;
    public Guid CategoryId { get; private set; } = categoryId;
    public TransactionType Type { get; private set; } = type;
    public bool IsRecurring { get; private set; } = isRecurring;
    public PaymentMethod PaymentMethod { get; private set; } = paymentMethod;
    public string? Merchant { get; private set; } = merchant;
}
