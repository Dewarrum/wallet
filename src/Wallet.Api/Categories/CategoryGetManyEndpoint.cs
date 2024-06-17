using Wallet.Api.Utils;
using Wallet.Application.Categories;

namespace Wallet.Api.Categories;

public static class CategoryGetManyEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet(
            "",
            async (
                Guid userId,
                ICategoryService categoryService,
                CancellationToken cancellationToken
            ) =>
            {
                var categories = await categoryService.GetForUser(userId, cancellationToken);

                return Results.Json(
                    new PageResponse<CategoryDto>(categories.Select(CategoryDto.From).ToList())
                );
            }
        );
    }
}
