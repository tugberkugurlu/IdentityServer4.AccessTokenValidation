using Microsoft.AspNet.Authentication;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer4.AccessTokenValidation
{
    public class IdentityServerBearerTokenOptions : AuthenticationOptions
    {
        public IdentityServerBearerTokenOptions()
        {
            AdditionalScopes = Enumerable.Empty<string>();
        }

        public IEnumerable<string> AdditionalScopes { get; set; }

        /// <summary>
        /// Gets or sets the base address of identity server (required)
        /// </summary>
        /// <value>
        /// The authority.
        /// </value>
        public string Authority { get; set; }

        /// <summary>
        /// Gets or sets the scope name for accessing the introspection endpoint.
        /// </summary>
        /// <value>
        /// The scope name.
        /// </value>
        public string ScopeName { get; set; }

        /// <summary>
        /// Gets or sets the scope secret for accessing the introspection endpoint.
        /// </summary>
        /// <value>
        /// The scope secret.
        /// </value>
        public string ScopeSecret { get; set; }
    }
}
