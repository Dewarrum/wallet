using Wallet.Domain.Categories;

namespace Wallet.Application.Categories;

public interface ICategoryService
{
    Task<Category> Create(
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<Category>> GetForUser(
        Guid userId,
        CancellationToken cancellationToken = default
    );
}
