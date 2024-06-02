using Wallet.Domain.Users;

namespace Wallet.Api.Accounts;

public sealed record AccountDto(Guid Id, string Name, string Email, DateTime CreatedAt)
{
    public static AccountDto From(User user) => new(user.Id, user.Name, user.Email, user.CreatedAt);
}
