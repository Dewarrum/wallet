namespace Wallet.Application.Profiles;

public sealed record CreateProfileRequest(Guid UserId, string? Currency);
