using Microsoft.AspNet.Authentication;
using System.Net.Http;

namespace IdentityServer4.AccessTokenValidation
{
    public class IntrospectionEndpointOptions : AuthenticationOptions
    {
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

        public HttpMessageHandler IntrospectionHttpHandler { get; set; }
    }
}
