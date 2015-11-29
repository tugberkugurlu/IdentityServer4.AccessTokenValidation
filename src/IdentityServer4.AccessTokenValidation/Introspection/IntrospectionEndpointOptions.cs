﻿using Microsoft.AspNet.Authentication;
using System;

namespace IdentityServer4.AccessTokenValidation
{
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
}
