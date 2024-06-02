namespace Wallet.Application.Utils;

public abstract class ResourceNotFoundException(string resourceName, string resourceId)
    : Exception($"{resourceName} '{resourceId}' not found");
