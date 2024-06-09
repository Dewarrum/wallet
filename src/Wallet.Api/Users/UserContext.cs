using System.Security.Claims;
using Wallet.Application.Users;
using Wallet.Domain.Users;

namespace Wallet.Api.Users;

internal sealed class UserContext(
    IHttpContextAccessor httpContextAccessor,
    IUserService userService
) : IUserContext
{
    public async Task<User> Get(CancellationToken cancellationToken = default)
    {
        var email = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
        return await userService.GetByEmail(email, cancellationToken);
    }
}
