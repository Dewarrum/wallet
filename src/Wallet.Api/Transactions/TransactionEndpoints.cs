namespace Wallet.Api.Transactions;

public static class TransactionEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("transactions").WithTags("transactions");

        TransactionCreateEndpoint.Map(group);
        TransactionGetEndpoint.Map(group);
        TransactionGetManyEndpoint.Map(group);
    }
}
