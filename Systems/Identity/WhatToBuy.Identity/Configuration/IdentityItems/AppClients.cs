namespace WhatToBuy.Identity.Configuration;

using WhatToBuy.Common.Security;
using Duende.IdentityServer.Models;
using Duende.IdentityServer;
using IdentityModel;

public static class AppClients
{
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "swagger",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                AccessTokenLifetime = 3600, // 1 hour

                RedirectUris = { "http://localhost:5074/swagger/o2c.html" },

                AllowedScopes = 
                {
                    AppScopes.Api
                }
            },
            new Client
            {
                ClientId = "frontend",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                AllowOfflineAccess = true,
                AccessTokenType = AccessTokenType.Jwt,

                AccessTokenLifetime = 3600, // 1 hour

                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AbsoluteRefreshTokenLifetime = 2592000, // 30 days
                SlidingRefreshTokenLifetime = 1296000, // 15 days

                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    AppScopes.Api,
                    JwtClaimTypes.Role,
                    AppClaims.FamilyIdClaim,
                    AppClaims.UserNameClaim
                }
            }
        };
}