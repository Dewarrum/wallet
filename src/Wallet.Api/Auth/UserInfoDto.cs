using Wallet.Domain.Users;

namespace Wallet.Api.Auth;

public sealed record UserInfoDto(Guid Id, string Name, string Email)
{
    public static UserInfoDto From(User user) => new(user.Id, user.Name, user.Email);
}
