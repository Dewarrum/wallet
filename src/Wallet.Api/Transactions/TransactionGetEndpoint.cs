using Wallet.Application.Transactions;

namespace Wallet.Api.Transactions;

public static class TransactionGetEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet(
            "{id:guid}",
            async (
                Guid id,
                ITransactionService transactionService,
                CancellationToken cancellationToken
            ) =>
            {
                try
                {
                    var transaction = await transactionService.Get(id, cancellationToken);
                    return Results.Json(transaction);
                }
                catch (TransactionNotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
            }
        );
    }
}
