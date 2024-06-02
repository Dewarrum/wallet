namespace Wallet.Api.Utils;

public sealed record PageResponse<TItem>(IReadOnlyList<TItem> Items);
