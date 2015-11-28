namespace IdentityServer4.AccessTokenValidation
{
    /// <summary>
    /// Enum for specifying where to validate the access token
    /// </summary>
    public enum ValidationMode
    {
        /// <summary>
        /// Use local validation for JWTs and the validation endpoint for reference tokens
        /// </summary>
        Both,

        /// <summary>
        /// Use local validation oly (only suitable for JWT tokens)
        /// </summary>
        Local,

        /// <summary>
        /// Use the validation endpoint only (works for both JWT and reference tokens)
        /// </summary>
        ValidationEndpoint
    }
}
