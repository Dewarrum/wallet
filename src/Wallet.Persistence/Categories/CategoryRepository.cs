using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Categories;

namespace Wallet.Persistence.Categories;

internal sealed class CategoryRepository(WalletDbContext dbContext) : ICategoryRepository
{
    public async Task Add(Category category, CancellationToken cancellationToken = default)
    {
        await dbContext.Categories.AddAsync(category, cancellationToken);
    }

    public async Task<Category?> Get(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Categories.FirstOrDefaultAsync(
            c => c.Id == categoryId,
            cancellationToken
        );
    }

    public async Task<IReadOnlyList<Category>> GetForUser(
        Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        return await dbContext
            .Categories.Where(c => c.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public void Remove(Category category)
    {
        dbContext.Remove(category);
    }
}
