using Wallet.Application.Transactions;

namespace Wallet.Api.Transactions;

public static class TransactionCreateEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapPost(
            "",
            async (
                CreateTransactionRequest request,
                ITransactionService transactionService,
                CancellationToken cancellationToken
            ) =>
            {
                var transaction = await transactionService.Create(request, cancellationToken);
                return Results.Json(TransactionDto.From(transaction));
            }
        );
    }
}
