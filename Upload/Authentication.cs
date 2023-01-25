using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace UploadAPI
{
    public class Authentication : IAuthentication
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _configuration;
        private readonly IRequestHandler _requestHandler;

        public Authentication(ILogger<Authentication> logger, IHttpClientFactory httpFactory, IConfiguration configuration, IRequestHandler requestHandler)
        {
            _logger = logger;
            _httpFactory = httpFactory;
            _configuration = configuration;
            _requestHandler = requestHandler;
        }

        public string GetAccessToken()
        {
            var encodedClientId = HttpUtility.UrlEncode(_configuration.GetValue<string>("ApiClientKey"));
            var encodedClientSecret = HttpUtility.UrlEncode(_configuration.GetValue<string>("ApiClientSecret"));

            var httpClient = _httpFactory.CreateClient("FotoWebApi");
            var tokenRequestString = $"grant_type=client_credentials&client_id={encodedClientId}&client_secret={encodedClientSecret}";
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{httpClient.BaseAddress}oauth2/token"),
                Content = new StringContent(tokenRequestString),
            };

            requestMessage.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" };

            var authenticationResult = httpClient.SendAsync(requestMessage).Result;
            _logger.LogInformation($"Request Id: {_requestHandler.GetRequestId(authenticationResult)}");
            var authenticationData = authenticationResult.Content.ReadAsStringAsync().Result;
            return JObject.Parse(authenticationData)["access_token"].ToString();
        }
    }
}
