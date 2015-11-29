using System;

namespace IdentityServer4.AccessTokenValidation
{
    /// <summary>
    /// Default clock implementation based on DateTimeOffset
    /// </summary>
	public class SystemClock : IClock
    {
        /// <summary>
        /// Gets the current UTC date/time 
        /// </summary>
        /// <value>
        /// UtcNow
        /// </value>
		public DateTimeOffset UtcNow
        {
            get { return DateTimeOffset.UtcNow; }
        }
    }
}
