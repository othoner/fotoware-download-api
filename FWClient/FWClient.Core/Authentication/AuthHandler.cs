using System.Net.Http.Headers;

namespace FWClient.Core.Authentication
{
    /// <inheritdoc/>
    public class AuthHandler : DelegatingHandler
    {
        private readonly ITokenProvider _tokenProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthHandler"/> class.
        /// </summary>
        /// <param name="tokenProvider">Token provider instance.</param>
        public AuthHandler(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var token = await _tokenProvider.GetTokenAsync();

            request.Headers.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}