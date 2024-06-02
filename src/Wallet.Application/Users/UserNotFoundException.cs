using Wallet.Application.Utils;
using Wallet.Domain.Users;

namespace Wallet.Application.Users;

public sealed class UserNotFoundException : ResourceNotFoundException
{
    public UserNotFoundException(Guid id)
        : base(nameof(User), id.ToString("N")) { }

    public UserNotFoundException(string email)
        : base(nameof(User), email) { }
}
