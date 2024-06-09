using Wallet.Api.Users;
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
                ICategoryService categoryService,
                IUserContext userContext,
                CancellationToken cancellationToken
            ) =>
            {
                var user = await userContext.Get(cancellationToken);
                var categories = await categoryService.GetForUser(user.Id, cancellationToken);

                return Results.Json(
                    new PageResponse<CategoryDto>(categories.Select(CategoryDto.From).ToList())
                );
            }
        );
    }
}