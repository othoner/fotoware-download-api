namespace FWClient.Core.BackgroundTasks.ResultFactory;

public interface IBackgroundTaskRequestBuilder
{
    HttpRequestMessage GenerateRequest(RequestedTaskInfo taskInfo);
}