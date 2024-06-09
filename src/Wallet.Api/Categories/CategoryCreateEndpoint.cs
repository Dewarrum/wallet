using Microsoft.AspNetCore.Mvc;
using Wallet.Api.Users;
using Wallet.Application.Categories;

namespace Wallet.Api.Categories;

public static class CategoryCreateEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapPost(
            "",
            async (
                [FromBody] CreateCategoryDto dto,
                ICategoryService categoryService,
                IUserContext userContext,
                CancellationToken cancellationToken
            ) =>
            {
                var user = await userContext.Get(cancellationToken);

                var request = new CreateCategoryRequest(user.Id, dto.Name, dto.Description);
                var category = await categoryService.Create(request, cancellationToken);

                return Results.Json(CategoryDto.From(category));
            }
        );
    }
}
