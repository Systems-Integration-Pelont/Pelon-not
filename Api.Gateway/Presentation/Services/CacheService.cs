using Microsoft.Extensions.Caching.Distributed;

namespace Presentation.Services;

internal sealed class CacheService(IDistributedCache distributedCache)
{
    public async Task<bool> IsTokenBlacklistedAsync(
        string token,
        CancellationToken cancellationToken = default
    )
    {
        string? result = await distributedCache.GetStringAsync(
            $"blacklist:{token}",
            cancellationToken
        );

        return !string.IsNullOrEmpty(result);
    }
}
