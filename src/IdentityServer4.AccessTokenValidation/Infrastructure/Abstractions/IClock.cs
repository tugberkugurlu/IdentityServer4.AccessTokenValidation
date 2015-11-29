using System;

namespace IdentityServer4.AccessTokenValidation
{
    /// <summary>
    /// Interface to abstract the clock
    /// </summary>
	public interface IClock
    {
        /// <summary>
        /// Gets the UTC now.
        /// </summary>
        /// <value>
        /// The UTC now.
        /// </value>
		DateTimeOffset UtcNow { get; }
    }
}
