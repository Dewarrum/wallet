namespace Wallet.Application.Categories;

public sealed record CreateCategoryRequest(Guid UserId, string Name, string Description);
