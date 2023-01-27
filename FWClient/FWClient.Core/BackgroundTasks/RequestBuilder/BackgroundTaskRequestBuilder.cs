using FWClient.Core.BackgroundTasks.ResultFactory;

namespace FWClient.Core.BackgroundTasks.RequestBuilder
{
    internal class BackgroundTaskRequestBuilder : IBackgroundTaskRequestBuilder
    {
        private readonly List<IRequestModifier> _modifiers = new ()
        {
            new GetUploadStatusRequestModifier(),
            new RenditionRequestModifier()
        };

        public HttpRequestMessage GenerateRequest(RequestedTaskInfo taskInfo)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get
            };

            foreach (var modifier in _modifiers)
            {
                modifier.Modify(taskInfo, request);
            }

            return request;
        }
    }
}