using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer4Demo
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "Demo API")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) }
                },

                // PolicyServer demo
                new ApiResource("policyserver.runtime"),
                new ApiResource("policyserver.management")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA (Code + PKCE)",

                    RequireClientSecret = false,
                    RequireConsent = true,

                    RedirectUris = { "https://notused" },
                    PostLogoutRedirectUris = { "https://notused" },

                    AlwaysIncludeUserClaimsInIdToken = true,

                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { "openid", "profile", "email", "api" },

                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,

                    // For testing purpose we are using different life times
                    AccessTokenLifetime = 60 * 10,
                    IdentityTokenLifetime = 60 * 20,
                },

                // implicit (e.g. SPA or OIDC authentication)
                new Client
                {
                    ClientId = "implicit",
                    ClientName = "Implicit Client",
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    AlwaysIncludeUserClaimsInIdToken = true,

                    // For testing purpose we are using different life times
                    AccessTokenLifetime = 60 * 10,
                    IdentityTokenLifetime = 60 * 20,

                    RedirectUris = { "https://notused" },
                    PostLogoutRedirectUris = { "https://notused" },
                    FrontChannelLogoutUri = "http://localhost:5000/signout-idsrv", // for testing identityserver on localhost

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "profile", "email", "api" },
                },

            };
        }
    }
}
