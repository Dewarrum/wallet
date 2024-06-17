namespace Wallet.Api.Users;

public sealed record UserInfoDto(Guid Id, string Name, string Email)
{
    public static UserInfoDto From(Domain.Users.User user) => new(user.Id, user.Name, user.Email);
}
