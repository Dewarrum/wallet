using Wallet.Domain.Users;

namespace Wallet.Application.Users;

public interface IUserService
{
    Task<User> Register(RegisterUserRequest request, CancellationToken cancellationToken = default);
    Task<User> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<User> GetByEmail(string email, CancellationToken cancellationToken = default);
}
