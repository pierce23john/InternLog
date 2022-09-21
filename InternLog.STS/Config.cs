// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace InternLog.STS
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> IdentityResources =>
				   new IdentityResource[]
				   {
						new IdentityResources.OpenId(),
						new IdentityResources.Profile(),
				   };

		public static IEnumerable<ApiResource> ApiResources =>
				new List<ApiResource>
				{
								new ApiResource("internlog-api", "Internlog API")
								{
									Scopes = { "internlog_api" },
								}
				};

		public static IEnumerable<ApiScope> ApiScopes =>
			new ApiScope[]
			{
				new ApiScope("scope1"),
				new ApiScope("internlog_api")
			};

		public static IEnumerable<Client> Clients =>
			new Client[]
			{
				// m2m client credentials flow client
				new Client
				{
					ClientId = "m2m.client",
					ClientName = "Client Credentials Client",

					AllowedGrantTypes = GrantTypes.ClientCredentials,
					ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

					AllowedScopes = { "scope1" }
				},

				// interactive client using code flow + pkce
				new Client
				{
					ClientId = "internlog.clientapp",
					ClientSecrets = { new Secret("testingsecret".Sha256()) },
					AllowedGrantTypes = GrantTypes.Code,
					RequireClientSecret = false,
					RedirectUris = { "http://localhost:4200/identity/login-callback" },
					FrontChannelLogoutUri = "http://localhost:4200/identity/logout-callback",
					PostLogoutRedirectUris = { "http://localhost:4200/identity/logout-callback" },
					AllowedCorsOrigins = {"http://localhost:4200"},

					AllowOfflineAccess = true,
					AllowedScopes = { "openid", "profile", "offline_access", "internlog_api" }
				},
			};
	}
}