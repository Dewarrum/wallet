using Wallet.Application.Utils;
using Wallet.Domain.Profiles;

namespace Wallet.Application.Profiles;

public sealed class ProfileNotFoundException(Guid id)
    : ResourceNotFoundException(nameof(Profile), id.ToString("N"));
