using Microsoft.EntityFrameworkCore;
using Npgsql;
using Wallet.Domain.Users;
using Wallet.Persistence.UnitOfWork;
using Wallet.Persistence.Users;

namespace Wallet.Application.Users;

internal sealed class UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : IUserService
{
    public async Task<User> Register(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var user = new User(Guid.NewGuid(), request.Name, request.Email, DateTime.UtcNow);

        try
        {
            await userRepository.Add(user, cancellationToken);
            await unitOfWork.SaveChanges(cancellationToken);
        }
        catch (DbUpdateException e)
            when (e.InnerException
                    is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation }
            )
        {
            throw new UserAlreadyExistsException(request.Email);
        }

        return user;
    }

    public async Task<User> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetById(id, cancellationToken);
        if (user is null)
            throw new UserNotFoundException(id);

        return user;
    }

    public async Task<User> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByEmail(email, cancellationToken);
        if (user is null)
            throw new UserNotFoundException(email);

        return user;
    }
}
