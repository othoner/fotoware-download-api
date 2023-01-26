using FWClient.Core.BackgroundTasks.ResultFactory;

namespace FWClient.Core.BackgroundTasks.RequestBuilder;

internal class BackgroundTaskRequestBuilder : IBackgroundTaskRequestBuilder
{
    private readonly List<IRequestModifier> Modifiers = new ()
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

        foreach (var modifier in Modifiers)
        {
            modifier.Modify(taskInfo, request);
        }

        return request;
    }
}