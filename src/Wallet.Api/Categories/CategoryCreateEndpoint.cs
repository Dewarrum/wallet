using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Categories;

namespace Wallet.Api.Categories;

public static class CategoryCreateEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapPost(
            "",
            async (
                [FromBody] CreateCategoryRequest request,
                ICategoryService categoryService,
                CancellationToken cancellationToken
            ) =>
            {
                var category = await categoryService.Create(request, cancellationToken);

                return Results.Json(CategoryDto.From(category));
            }
        );
    }
}
