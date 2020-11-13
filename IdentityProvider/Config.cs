// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace IdentityProvider
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                //new IdentityResource
                //{
                //    Name = "roles",
                //    DisplayName = "Roles",
                //    UserClaims = { JwtClaimTypes.Role }
                //}
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
              //  new ApiResource(IdentityServerConstants.LocalApi.ScopeName, "Local Api", new [] { JwtClaimTypes.Role }),
                new ApiResource("backend", "MarketPlace REST API", new [] { JwtClaimTypes.Role,JwtClaimTypes.Name}),
            };
        }

        //public static IEnumerable<Scope> ssss()
        //{
        //    return new List<Scope>
        //    {
        //    IdentityServerConstants.StandardScopes.OpenId,
        //    IdentityServerConstants.StandardScopes.Profile,

        //    new Scope
        //    {
        //        Name = "api1",
        //        Description = "My API"
        //    },
        //    new Scope
        //    {
        //        Enabled = true,
        //        Name  = "role",
        //        DisplayName = "Role(s)",
        //        Description = "roles of user",
        //        Type = ScopeType.Identity,
        //        Claims = new List<ScopeClaim>
        //        {
        //            new ScopeClaim("role",false)
        //        }
        //    },
        //    StandardScopes.AllClaims
        //    };
        //}

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "frontend",
                    ClientName = "MarketPlace JavaScript Client",
                    ClientUri = "http://localhost:8080",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 90, // 1.5 minutes
                    AbsoluteRefreshTokenLifetime = 0,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    RequireConsent = false,
                    ClientClaimsPrefix = string.Empty,
                 
                    RedirectUris =
                    {
                        "http://localhost:8080/callback",
                        "http://localhost:8080/static/silent-renew.html"
                    },

                    PostLogoutRedirectUris = { "http://localhost:8080" },
                    AllowedCorsOrigins = { "http://localhost:8080" },
                  //  AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes = {
                                       IdentityServerConstants.LocalApi.ScopeName,
                                       IdentityServerConstants.StandardScopes.OpenId,
                                       IdentityServerConstants.StandardScopes.Profile,
                                       IdentityServerConstants.StandardScopes.Email,
                                       IdentityServerConstants.StandardScopes.Phone,
                                     //  "roles",
                                       "backend" }
                }
            };
        }
    }
}