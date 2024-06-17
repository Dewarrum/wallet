using Wallet.Domain.Categories;

namespace Wallet.Api.Categories;

public sealed record CategoryDto(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    DateTime CreatedAt
)
{
    public static CategoryDto From(Category category) =>
        new(category.Id, category.UserId, category.Name, category.Description, category.CreatedAt);
}
