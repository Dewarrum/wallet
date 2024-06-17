using Wallet.Domain.Categories;
using Wallet.Persistence.Categories;
using Wallet.Persistence.UnitOfWork;

namespace Wallet.Application.Categories;

internal sealed class CategoryService(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork
) : ICategoryService
{
    public async Task<Category> Create(
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var category = new Category(
            Guid.NewGuid(),
            request.UserId,
            request.Name,
            request.Description,
            DateTime.UtcNow
        );

        await categoryRepository.Add(category, cancellationToken);
        await unitOfWork.SaveChanges(cancellationToken);

        return category;
    }

    public async Task<IReadOnlyList<Category>> GetForUser(
        Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        return await categoryRepository.GetForUser(userId, cancellationToken);
    }
}