using System.Security.Authentication;
using FWClient.Core.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FWClient.Core.Authentication
{
    public class TokenProvider : ITokenProvider
    {
        private readonly HttpClient _httpClient;
        private readonly FotoWebConfigs _configs;
        private readonly Lazy<Task<AuthenticationResult>> _lazyAuthentication;

        public TokenProvider(IOptions<FotoWebConfigs> configs, HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configs = configs?.Value ?? throw new ArgumentNullException(nameof(configs));
            _lazyAuthentication = new Lazy<Task<AuthenticationResult>>(RequestAccessToken);
        }

        public async Task<AuthenticationResult> GetTokenAsync()
        {
            return await _lazyAuthentication.Value;
        }

        private async Task<AuthenticationResult> RequestAccessToken()
        {
            var form = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", _configs.ClientId },
                { "client_secret", _configs.ClientSecret },
            };

            var requestUri = new Uri(new Uri(_configs.BaseAddress), "fotoweb/oauth2/token");
            var requestResult = await _httpClient.PostAsync(requestUri, new FormUrlEncodedContent(form));

            if (!requestResult.IsSuccessStatusCode)
            {
                throw new AuthenticationException("Fail to authenticate.");
            }

            var json = await requestResult.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthenticationResult>(json);

            return result!;
        }
    }
}