using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer4.AccessTokenValidation
{
    public static class AccessTokenValidationApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseIdentityServerBearerTokenAuthentication(this IApplicationBuilder app, IdentityServerBearerTokenOptions options, IntrospectionEndpointOptions introspectionEndpointOptions)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (introspectionEndpointOptions == null)
            {
                throw new ArgumentNullException(nameof(introspectionEndpointOptions));
            }

            // TODO: Not sure what to do here.
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

            app.UseMiddleware<IntrospectionEndpointMiddleware>(introspectionEndpointOptions);

            if (options.AdditionalScopes.Any())
            {
                IEnumerable<string> scopes = options.AdditionalScopes.Concat(new[] { introspectionEndpointOptions.ScopeName });
                app.UseMiddleware<ScopeRequirementMiddleware>(scopes);
            }

            return app;
        }
    }
}
