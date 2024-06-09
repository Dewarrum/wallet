using Wallet.Domain.Users;

namespace Wallet.Api.Users;

public interface IUserContext
{
    Task<User> Get(CancellationToken cancellationToken = default);
}
