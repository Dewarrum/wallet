using Wallet.Domain.Categories;

namespace Wallet.Persistence.Categories;

public interface ICategoryRepository
{
    Task Add(Category category, CancellationToken cancellationToken = default);

    Task<Category?> Get(Guid categoryId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Category>> GetForUser(
        Guid userId,
        CancellationToken cancellationToken = default
    );

    void Remove(Category category);
}
