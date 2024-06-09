namespace Wallet.Api.Categories;

public static class CategoryEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("categories").WithTags("categories");

        CategoryCreateEndpoint.Map(group);
        CategoryGetManyEndpoint.Map(group);
    }
}
