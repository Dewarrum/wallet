using Wallet.Api.Utils;
using Wallet.Application.Transactions;

namespace Wallet.Api.Transactions;

public static class TransactionGetManyEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet(
            "",
            async (
                Guid profileId,
                int? skip,
                int? take,
                ITransactionService transactionService,
                CancellationToken cancellationToken
            ) =>
            {
                var transactions = await transactionService.GetForProfile(
                    profileId,
                    skip ?? 0,
                    take ?? 100,
                    cancellationToken
                );

                return Results.Json(
                    new PageResponse<TransactionDto>(
                        transactions.Select(TransactionDto.From).ToList()
                    )
                );
            }
        );
    }
}
