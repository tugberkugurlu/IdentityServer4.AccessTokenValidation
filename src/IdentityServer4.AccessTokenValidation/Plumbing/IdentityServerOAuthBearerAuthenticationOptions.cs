using Microsoft.AspNet.Authentication.JwtBearer;

namespace IdentityServer4.AccessTokenValidation.Plumbing
{
    /// <summary>
    /// Options that wraps OAuth2BearerAuthenticationOptions for local and remote token validation
    /// </summary>
    public class IdentityServerOAuthBearerAuthenticationOptions
    {
        /// <summary>
        /// Gets or sets the local validation options.
        /// </summary>
        /// <value>
        /// The local validation options.
        /// </value>
        public JwtBearerOptions LocalValidationOptions { get; set; }

        /// <summary>
        /// Gets or sets the endpoint validation options.
        /// </summary>
        /// <value>
        /// The endpoint validation options.
        /// </value>
        public JwtBearerOptions EndpointValidationOptions { get; set; }
    }
}
