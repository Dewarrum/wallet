namespace Wallet.Domain.Profiles;

public sealed class Profile(Guid id, Guid userId, string currency, DateTime createdAt)
{
    private Profile()
        : this(default, default, default!, default) { }

    public Guid Id { get; init; } = id;
    public Guid UserId { get; init; } = userId;
    public string Currency { get; private set; } = currency;
    public DateTime CreatedAt { get; init; } = createdAt;
}
