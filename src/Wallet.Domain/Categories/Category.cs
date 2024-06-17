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

    public Guid Id { get; init; } = id;
    public Guid UserId { get; init; } = userId;
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public DateTime CreatedAt { get; init; } = createdAt;
}
