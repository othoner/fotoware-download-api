using Microsoft.Extensions.Logging;

namespace FWClient.Core.Tracing
{
    internal class RequestIdHandler : DelegatingHandler
    {
        private const string XRequestIdHeaderName = "X-RequestId";
        private const string FotoWebServerHeaderName = "FotoWeb-Server";

        private readonly ILogger<RequestIdHandler> _logger;

        public RequestIdHandler(ILogger<RequestIdHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpResponseMessage = base.Send(request, cancellationToken);

            _logger.LogInformation($"Request Id: {GetRequestId(httpResponseMessage)}");
            _logger.LogInformation($"FotoWeb-Server: {GetFotoWebServer(httpResponseMessage)}");

            return httpResponseMessage;
        }

        private static string GetRequestId(HttpResponseMessage responseMessage)
        {
            if (responseMessage.Headers.TryGetValues(XRequestIdHeaderName, out var requestIdValues))
            {
                var firstRequestId = requestIdValues.FirstOrDefault();
                if (firstRequestId != null)
                {
                    return firstRequestId;
                }
            }

            return "Could not find request id";
        }
        
        private static string GetFotoWebServer(HttpResponseMessage responseMessage)
        {
            if (responseMessage.Headers.TryGetValues(FotoWebServerHeaderName, out var headers))
            {
                var webServer = headers.FirstOrDefault();
                if (webServer != null)
                {
                    return webServer;
                }
            }

            return "Could not find server header";
        }
    }
}