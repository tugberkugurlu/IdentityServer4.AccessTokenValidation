﻿using System;

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
}
