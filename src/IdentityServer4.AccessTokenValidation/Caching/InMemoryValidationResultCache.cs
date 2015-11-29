using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using IdentityModel;

namespace IdentityServer4.AccessTokenValidation
{
    public class InMemoryValidationResultCache : IValidationResultCache
    {
        private const string CacheKeyPrefix = "identityserver4:token:";
        private readonly IdentityServerBearerTokenOptions _options;
        private readonly IMemoryCache _cache;
        private readonly IClock _clock;

        public InMemoryValidationResultCache(IdentityServerBearerTokenOptions options, IMemoryCache cache, IClock clock)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (clock == null)
            {
                throw new ArgumentNullException(nameof(clock));
            }

            _options = options;
            _cache = cache;
            _clock = clock;
        }

        public Task AddAsync(string token, IEnumerable<Claim> claims)
        {
            var expiryClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Expiration);
            var cacheExpirySetting = _clock.UtcNow.Add(_options.ValidationResultCacheDuration);

            if (expiryClaim != null)
            {
                long epoch;
                if (long.TryParse(expiryClaim.Value, out epoch))
                {
                    var tokenExpiresAt = epoch.ToDateTimeOffsetFromEpoch();
                    if (tokenExpiresAt < cacheExpirySetting)
                    {
                        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(tokenExpiresAt);
                        _cache.Set($"{CacheKeyPrefix}{token}", claims, cacheOptions);
                    }
                }
            }
            else
            {
                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(cacheExpirySetting);
                _cache.Set($"{CacheKeyPrefix}{token}", claims, cacheOptions);
            }

            return Task.FromResult(0);
        }

        public Task<IEnumerable<Claim>> GetAsync(string token)
        {
            IEnumerable<string> claims = null;
            if(_cache.TryGetValue($"{CacheKeyPrefix}{token}", out claims))
            {
            }

            throw new NotImplementedException();
        }
    }
}
