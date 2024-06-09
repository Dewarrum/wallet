using Wallet.Api.Users;
using Wallet.Api.Utils;
using Wallet.Application.Profiles;
using Wallet.Application.Transactions;
using Wallet.Domain.Profiles;

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
                IProfileService profileService,
                IUserContext userContext,
                CancellationToken cancellationToken
            ) =>
            {
                var user = await userContext.Get(cancellationToken);

                Profile profile;
                try
                {
                    profile = await profileService.Get(profileId, cancellationToken);
                }
                catch (ProfileNotFoundException)
                {
                    return Results.NotFound();
                }

                if (profile.UserId != user.Id)
                    return Results.NotFound();

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
