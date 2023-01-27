namespace FWClient.Core.BackgroundTasks.RequestBuilder
{
    /// <summary>
    /// Modify request based on background task properties.
    /// </summary>
    internal interface IRequestModifier
    {
        /// <summary>
        /// Modify request based on background task properties.
        /// </summary>
        /// <param name="taskInfo">Background task description.</param>
        /// <param name="requestMessage">Request message to modify.</param>
        void Modify(RequestedTaskInfo taskInfo, HttpRequestMessage requestMessage);
    }
}