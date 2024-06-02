using Wallet.Application.Transactions;

namespace Wallet.Api.Transactions;

public static class TransactionEndpoints
{
    public static async Task<IResult> Get(
        Guid id,
        ITransactionService transactionService,
        CancellationToken cancellationToken
    )
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

    public static async Task<IResult> Create(
        CreateTransactionRequest request,
        ITransactionService transactionService,
        CancellationToken cancellationToken
    )
    {
        var transaction = await transactionService.Create(request, cancellationToken);
        return Results.Json(TransactionDto.From(transaction));
    }
}
