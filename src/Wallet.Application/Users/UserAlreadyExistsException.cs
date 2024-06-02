namespace Wallet.Application.Users;

public sealed class UserAlreadyExistsException(string email)
    : Exception($"User with '{email}' email already exists");
