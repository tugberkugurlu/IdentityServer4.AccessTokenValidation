using Microsoft.AspNet.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer4.AccessTokenValidation
{
    public class IdentityServerBearerTokenOptions : AuthenticationOptions
    {
        public IdentityServerBearerTokenOptions()
        {
            AuthenticationScheme = "Bearer";
            ValidationMode = ValidationMode.Both;
            AdditionalScopes = Enumerable.Empty<string>();
            ValidationResultCacheDuration = TimeSpan.FromMinutes(5);
            PreserveAccessToken = false;
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
        /// Gets or sets a value indicating whether to preserve the access token as a claim. Defaults to false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if access token is preserved; otherwise, <c>false</c>.
        /// </value>
        public bool PreserveAccessToken { get; set; }   

        /// <summary>
        /// Gets or sets the validation mode.
        /// </summary>
        /// <value>
        /// The validation mode.
        /// </value>
        public ValidationMode ValidationMode { get; set; }

        /// <summary>
        /// Gets or sets the duration of the validation result cache.
        /// </summary>
        /// <value>
        /// The duration of the validation result cache. The default is 5 minutes.
        /// </value>
        public TimeSpan ValidationResultCacheDuration { get; set; }

        public IntrospectionEndpointOptions IntrospectionOptions { get; set; }
    }
}
