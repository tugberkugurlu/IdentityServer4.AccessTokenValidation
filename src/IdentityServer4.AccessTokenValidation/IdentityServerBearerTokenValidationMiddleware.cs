using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.AccessTokenValidation
{
    public class IdentityServerBearerTokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IdentityServerBearerTokenOptions _options;
        private readonly ILogger<IdentityServerBearerTokenValidationMiddleware> _logger;

        public IdentityServerBearerTokenValidationMiddleware(RequestDelegate next, IdentityServerBearerTokenOptions options, ILogger<IdentityServerBearerTokenValidationMiddleware> logger)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _next = next;
            _options = options;
            _logger = logger;
        }

        public Task Invoke(HttpContext context)
        {
            return _next.Invoke(context);
        }
    }
}
