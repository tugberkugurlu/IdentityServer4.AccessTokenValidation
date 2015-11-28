using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;
using System;

namespace IdentityServer4.AccessTokenValidation
{
    public class IdentityServerBearerTokenValidationMiddleware : AuthenticationMiddleware<IdentityServerBearerTokenOptions>
    {
        public IdentityServerBearerTokenValidationMiddleware(RequestDelegate next, IdentityServerBearerTokenOptions options, IUrlEncoder urlEncoder, ILoggerFactory loggerFactory)
            : base(next, options, loggerFactory, urlEncoder)
        {
        }

        protected override AuthenticationHandler<IdentityServerBearerTokenOptions> CreateHandler()
        {
            throw new NotImplementedException();
        }
    }
}
