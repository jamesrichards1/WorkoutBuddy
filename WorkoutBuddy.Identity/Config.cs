using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;
using WorkoutBuddy.Core;

namespace WorkoutBuddy.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "user",
                UserClaims =
                {
                    JwtClaimTypes.Id,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email,
                }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope(
                    name: Constants.Auth.ApiScopes.PublicApi, 
                    displayName: Constants.Auth.ApiScopes.PublicApiDisplayName)
            };

    public static IEnumerable<Client> Clients =>
        new Client[] 
            {
                new Client
                {
                    ClientId= Constants.Auth.Clients.ReactClient, // Should be for an asp.net server, not the JS client
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5002/signin-oidc" }, //URL of the client
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" }, //URL of the client
                    AllowedScopes = 
                    { 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        Constants.Auth.ApiScopes.PublicApi
                    }
                },
                new Client
                {
                    ClientId= Constants.Auth.Clients.DevelopmentClient,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(Constants.Auth.Clients.DevelopmentClientSecret.Sha256())
                    },
                    AllowedScopes = { Constants.Auth.ApiScopes.PublicApi }
                }
            };
}