namespace Wallet.Domain.Categories;

public sealed class Category(
    Guid id,
    Guid userId,
    string name,
    string description,
    DateTime createdAt
)
{
    private Category()
        : this(default, default, default!, default!, default) { }

    public Guid Id { get; } = id;
    public Guid UserId { get; } = userId;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public DateTime CreatedAt { get; } = createdAt;
}
