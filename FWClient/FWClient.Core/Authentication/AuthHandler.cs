using System.Net.Http.Headers;

namespace FWClient.Core.Authentication
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly ITokenProvider _tokenProvider;

        public AuthHandler(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenProvider.GetTokenAsync();

            request.Headers.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
