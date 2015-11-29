using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
