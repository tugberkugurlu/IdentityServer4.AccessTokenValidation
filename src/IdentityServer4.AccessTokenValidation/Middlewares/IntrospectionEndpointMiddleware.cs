using IdentityModel.Client;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.WebEncoders;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServer4.AccessTokenValidation
{
    public class IntrospectionEndpointHandler : AuthenticationHandler<IntrospectionEndpointOptions>
    {
        private const string BearerAuthSchema = "Bearer";
        private static readonly Lazy<HttpMessageHandler> _handler = new Lazy<HttpMessageHandler>(
            () => new HttpClientHandler(), isThreadSafe: true);

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            ValidateRequirements();

            // ctor

            var resultCache = Context.RequestServices.GetService<IValidationResultCache>();
            var logger = Context.RequestServices.GetService<ILogger<IntrospectionEndpointHandler>>();
            var introspectionEndpoint = $"{Options.Authority.EnsureTrailingSlash()}connect/introspect";
            var handler = Options.BackchannelHttpHandler ?? _handler.Value;

            IntrospectionClient introspectionClient = string.IsNullOrEmpty(Options.ScopeName) == false
                ? new IntrospectionClient(introspectionEndpoint, Options.ScopeName, Options.ScopeSecret ?? string.Empty, handler)
                : new IntrospectionClient(introspectionEndpoint, innerHttpMessageHandler: handler);

            // handle

            AuthenticateResult result;

            StringValues authorization;
            if(Request.Headers.TryGetValue("Authorization", out authorization) == false)
            {
                result = AuthenticateResult.Failed("No authorization header.");
            }
            else
            {
                if(authorization.ToString().StartsWith($"{BearerAuthSchema} ", StringComparison.OrdinalIgnoreCase) == false)
                {
                    return AuthenticateResult.Failed("Invalid or unsopported authentication schema.");
                }
                else
                {
                    var token = authorization.ToString().Substring($"{BearerAuthSchema} ".Length).Trim();
                    if (string.IsNullOrEmpty(token))
                    {
                        result = AuthenticateResult.Failed("No bearer token.");
                    }
                    else
                    {
                        result = await HandleImplAsync(token, logger);
                    }
                }
            }

            return result;
        }

        private async Task<AuthenticateResult> HandleImplAsync(string token, ILogger logger)
        {
            throw new NotImplementedException();
        }

        private void ValidateRequirements()
        {
            if (string.IsNullOrWhiteSpace(Options.Authority))
            {
                throw new InvalidOperationException("Authority must be set to use validation endpoint.");
            }
        }
    }

    public class IntrospectionEndpointMiddleware : AuthenticationMiddleware<IntrospectionEndpointOptions>
    {
        public IntrospectionEndpointMiddleware(RequestDelegate next, IntrospectionEndpointOptions options, IUrlEncoder urlEncoder, ILoggerFactory loggerFactory)
            : base(next, options, loggerFactory, urlEncoder)
        {
        }

        protected override AuthenticationHandler<IntrospectionEndpointOptions> CreateHandler()
        {
            return new IntrospectionEndpointHandler();
        }
    }
}
