namespace FWClient.Core.BackgroundTasks
{
    public class RequestedTaskInfo
    {
        public TaskType Type { get; set; }

        public string? TaskId { get; set; }
    }
}