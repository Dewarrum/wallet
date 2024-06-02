namespace Wallet.Domain.Users;

public sealed class User(Guid id, string name, string email, DateTime createdAt)
{
    private User()
        : this(default, default!, default!, default) { }

    public Guid Id { get; init; } = id;
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public DateTime CreatedAt { get; init; } = createdAt;
}
