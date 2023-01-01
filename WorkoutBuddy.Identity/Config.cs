using Duende.IdentityServer.Models;
using WorkoutBuddy.Core;

namespace WorkoutBuddy.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId()
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
                    ClientId= Constants.Auth.Clients.ReactClient,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { Constants.Auth.ApiScopes.PublicApi }
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