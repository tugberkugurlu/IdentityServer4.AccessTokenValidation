using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.AccessTokenValidation
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseIdentityServerBearerTokenAuthentication(this IApplicationBuilder app, IdentityServerBearerTokenOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            switch (options.ValidationMode)
            {
                case ValidationMode.Both:
                    break;

                case ValidationMode.Local:
                    break;

                case ValidationMode.ValidationEndpoint:
                    break;

                default:
                    throw new Exception("ValidationMode has invalid value");
            }

            if(options.IntrospectionOptions != null)
            {
                app.UseMiddleware<IntrospectionEndpointMiddleware>(options.IntrospectionOptions);
            }

            if (options.AdditionalScopes.Any())
            {
                IEnumerable<string> scopes = options.IntrospectionOptions != null
                    ? options.AdditionalScopes.Concat(new[] { options.IntrospectionOptions.ScopeName })
                    : options.AdditionalScopes;

                app.UseMiddleware<ScopeRequirementMiddleware>(scopes);
            }

            return app;
        }

        /// <summary>
        /// Middleware to check for scope claims in access token
        /// </summary>
        public class ScopeRequirementMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly IEnumerable<string> _scopes;

            /// <summary>
            /// Initializes a new instance of the <see cref="ScopeRequirementMiddleware"/> class.
            /// </summary>
            /// <param name="next">The next midleware.</param>
            /// <param name="scopes">The scopes.</param>
            public ScopeRequirementMiddleware(RequestDelegate next, IEnumerable<string> scopes)
            {
                if(next == null)
                {
                    throw new ArgumentNullException(nameof(next));
                }

                if(scopes == null)
                {
                    throw new ArgumentNullException(nameof(scopes));
                }

                _next = next;
                _scopes = scopes;
            }

            public Task Invoke(HttpContext context)
            {
                // if no token was sent - no need to validate scopes
                var principal = context.User;

                if(principal == null || principal.Identity == null | !principal.Identity.IsAuthenticated)
                {
                    return _next.Invoke(context);
                }
                else if (ScopesFound(context))
                {
                    return _next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = 403;
                    context.Response.Headers.Add(("WWW-Authenticate", new[] { "Bearer error=\"insufficient_scope\"" });

                    return Task.FromResult(0);
                }
            }

            private bool ScopesFound(HttpContext context)
            {
                var scopeClaims = context.User.FindAll("scope");

                if (scopeClaims == null || !scopeClaims.Any())
                {
                    return false;
                }

                foreach (var scope in scopeClaims)
                {
                    if (_scopes.Contains(scope.Value, StringComparer.Ordinal))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
