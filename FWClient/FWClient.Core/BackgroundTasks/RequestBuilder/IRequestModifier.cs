namespace FWClient.Core.BackgroundTasks.RequestBuilder;

internal interface IRequestModifier
{
    void Modify(RequestedTaskInfo taskInfo, HttpRequestMessage requestMessage);
}