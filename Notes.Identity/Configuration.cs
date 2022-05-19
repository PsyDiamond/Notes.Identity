using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Notes.Identity
{
    public static class Configuration
    {
        /// <summary>
        /// Область приложения, которую можно использовать
        /// </summary>
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("NotesWebApi", "Web API"),

            };

        /// <summary>
        /// Предоставляемые ресурсы
        /// </summary>
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                // Open ID
                new IdentityResources.OpenId(),
                // Профмль
                new IdentityResources.Profile()
            };

        /// <summary>
        /// Ресурсы приложения
        /// </summary>
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            { 
                new ApiResource("NotesWebAPI", "Web API", new []{JwtClaimTypes.Name})
                { 
                    Scopes = {"NotesWebAPI"}
                }
            };

        /// <summary>
        /// Клиенты
        /// </summary>
        public static IEnumerable<Client> Clients =>
            new List<Client> {
                new Client
                {
                    ClientId = "notes-web-api",
                    ClientName = "Notes Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http://.../signin-oidc"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://..."
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://.../signout-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "NotesWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
