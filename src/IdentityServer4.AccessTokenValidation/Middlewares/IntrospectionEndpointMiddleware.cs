using IdentityModel.Client;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.WebEncoders;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
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
                        result = await HandleImplAsync(token, cache, logger);
                    }
                }
            }

            return result;
        }

        private void ValidateRequirements()
        {
            if (string.IsNullOrWhiteSpace(Options.Authority))
            {
                throw new InvalidOperationException("Authority must be set to use validation endpoint.");
            }
        }

        private async Task<AuthenticateResult> HandleImplAsync(string token)
        {
            var cache = Context.RequestServices.GetService<IValidationResultCache>();

            if(cache != null)
            {
                var cachedClaims = await cache.GetAsync(token);
                if (cachedClaims != null)
                {
                }
            }
        }

        private async Task<AuthenticateResult> HandleRemoteAsync(string token, IValidationResultCache cache)
        {
            var introspectionEndpoint = $"{Options.Authority.EnsureTrailingSlash()}connect/introspect";
            var handler = Options.BackchannelHttpHandler ?? _handler.Value;

            IntrospectionClient introspectionClient = string.IsNullOrEmpty(Options.ScopeName) == false
                ? new IntrospectionClient(introspectionEndpoint, Options.ScopeName, Options.ScopeSecret ?? string.Empty, handler)
                : new IntrospectionClient(introspectionEndpoint, innerHttpMessageHandler: handler);

            IntrospectionResponse response = null;
            try
            {
                response = await introspectionClient.SendAsync(new IntrospectionRequest { Token = token });
            }
            catch (Exception ex)
            {
                Logger.LogError("Exception while contacting introspection endpoint.", ex);
            }

            if(response != null)
            {
                if (response.IsError)
                {
                    Logger.LogError("Error returned from introspection endpoint: {introspectionRequestError}.", response.Error);
                }
                else if (!response.IsActive)
                {
                    Logger.LogVerbose("Inactive token: {inactiveToken}", token);
                }
                else
                {
                    var claims = new List<Claim>();
                    foreach (var claim in response.Claims)
                    {
                        if (!string.Equals(claim.Item1, "active", StringComparison.Ordinal))
                        {
                            claims.Add(new Claim(claim.Item1, claim.Item2));
                        }
                    }

                    if(cache != null)
                    {
                        await cache.AddAsync(token, claims, Options.ValidationResultCacheDuration);
                    }
                }
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
