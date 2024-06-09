using Wallet.Api.Users;
using Wallet.Application.Profiles;
using Wallet.Application.Transactions;
using Wallet.Domain.Profiles;

namespace Wallet.Api.Transactions;

public static class TransactionGetEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet(
            "{id:guid}",
            async (
                Guid id,
                Guid profileId,
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
