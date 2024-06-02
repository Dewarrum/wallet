using System.Security.Claims;
using Wallet.Application.Users;
using Wallet.Domain.Users;

namespace Wallet.Api.Users;

internal sealed class UserContext(
    IHttpContextAccessor httpContextAccessor,
    IUserService userService
) : IUserContext
{
    public async Task<User> Get(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(
            httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
        );

        return await userService.GetById(userId, cancellationToken);
    }
}