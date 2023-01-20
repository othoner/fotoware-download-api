using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace DownloadAPI
{
    public class RequestHandler : IRequestHandler
    {
        public string GetRequestId(HttpResponseMessage responseMessage)
        {
            IEnumerable<string> requestIdValues;
            if (responseMessage.Headers.TryGetValues("X-RequestId", out requestIdValues))
            {
                var firstRequestId = requestIdValues.FirstOrDefault();
                if (firstRequestId != null)
                {
                    return firstRequestId;
                }
            }

            return "Could not find request id";
        }
    }
}
