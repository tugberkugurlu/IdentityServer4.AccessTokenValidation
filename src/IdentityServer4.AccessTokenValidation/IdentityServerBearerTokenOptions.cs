﻿using Microsoft.AspNet.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace IdentityServer4.AccessTokenValidation
{
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
