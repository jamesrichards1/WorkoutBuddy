using System.Text.Json;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorkoutBuddy.Core;

namespace WorkoutBuddy.TestAuthClient
{
    internal class AuthService : IHostedService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthService(ILogger<AuthService> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var publicApiEndpoint = GetPublicApiEndpoint();
            if (string.IsNullOrEmpty(publicApiEndpoint))
            {
                throw new Exception("Missing endpoint configuration for Public Api.");
            }

            var accessToken = await GetPublicApiAccessToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new Exception("Missing access token for Public Api.");
            }

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.SetBearerToken(accessToken);

            var identityResponse = await httpClient.GetAsync($"{publicApiEndpoint}/api/identity");
            if (!identityResponse.IsSuccessStatusCode)
            {
                throw new Exception("Public Api Identity request failed. Status: " + identityResponse.StatusCode);
            }
            var doc = JsonDocument.Parse(await identityResponse.Content.ReadAsStringAsync());
            var formattedResponse = JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true });
            _logger.LogInformation(formattedResponse);
        }

        private async Task<string> GetPublicApiAccessToken()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var authApiEndpoint = _configuration.GetValue<string>("WorkoutBuddy.Identity:Authority");
            if (string.IsNullOrEmpty(authApiEndpoint))
            {
                throw new NullReferenceException("Missing WorkoutBuddy.Identity:Authority");
            }

            var disco = await httpClient.GetDiscoveryDocumentAsync(authApiEndpoint);
            if (disco == null || disco.IsError)
            {
                throw new Exception(disco?.Error ?? "Missing discovery data");
            }

            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = Constants.Auth.Clients.DevelopmentClient,
                ClientSecret = Constants.Auth.Clients.DevelopmentClientSecret,
                Scope = Constants.Auth.ApiScopes.PublicApi
            });

            if (tokenResponse == null || tokenResponse.IsError)
            {
                throw new Exception(tokenResponse?.Error ?? "Failed to get client credentials from " + disco.TokenEndpoint);
            }

            return tokenResponse.AccessToken;
        }

        private string GetPublicApiEndpoint()
        {
            return _configuration.GetValue<string>("WorkoutBuddy.Api:Endpoint")!;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
