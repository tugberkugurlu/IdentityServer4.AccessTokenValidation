using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;
using System;

namespace IdentityServer4.AccessTokenValidation
{
    public class IntrospectionEndpointMiddleware : AuthenticationMiddleware<IntrospectionEndpointOptions>
    {
        public IntrospectionEndpointMiddleware(RequestDelegate next, IntrospectionEndpointOptions options, IUrlEncoder urlEncoder, ILoggerFactory loggerFactory)
            : base(next, options, loggerFactory, urlEncoder)
        {
        }

        protected override AuthenticationHandler<IntrospectionEndpointOptions> CreateHandler()
        {
            throw new NotImplementedException();
        }
    }
}
