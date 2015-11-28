using Microsoft.AspNet.Builder;
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
                    break;
            }

            return app;
        }
    }
}
