using Microsoft.AspNet.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace IdentityServer4.AccessTokenValidation
{
    internal interface IIdentityServerBearerTokenBaseOptions
    {
        /// <summary>
        /// Gets or sets the base address of identity server (required)
        /// </summary>
        /// <value>
        /// The authority.
        /// </value>
        string Authority { get; set; }

        /// <summary>
        /// Gets or sets the duration of the validation result cache.
        /// </summary>
        /// <value>
        /// The duration of the validation result cache. The default is 5 minutes.
        /// </value>
        TimeSpan ValidationResultCacheDuration { get; set; }
    }

    public class IntrospectionEndpointOptions : RemoteAuthenticationOptions, IIdentityServerBearerTokenBaseOptions
    {
        public IntrospectionEndpointOptions()
        {
            AuthenticationScheme = "Bearer";
            ValidationResultCacheDuration = TimeSpan.FromMinutes(5);
        }

        /// <summary>
        /// Gets or sets the scope name for accessing the introspection endpoint. This will also be used
        /// for the scope validation.
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

        public string Authority { get; set; }

        public TimeSpan ValidationResultCacheDuration { get; set; }
    }

    public class IdentityServerBearerTokenOptions : AuthenticationOptions, IIdentityServerBearerTokenBaseOptions
    {
        public IdentityServerBearerTokenOptions()
        {
            AuthenticationScheme = "Bearer";
            ValidationResultCacheDuration = TimeSpan.FromMinutes(5);
            ValidationMode = ValidationMode.Both;
            AdditionalScopes = Enumerable.Empty<string>();
            PreserveAccessToken = false;
        }

        public IEnumerable<string> AdditionalScopes { get; set; }

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

        public string Authority { get; set; }

        public TimeSpan ValidationResultCacheDuration { get; set; }
    }
}
