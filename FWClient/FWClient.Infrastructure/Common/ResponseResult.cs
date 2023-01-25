namespace FWClient.Core.Common
{
    public class ResponseResult<T>
    {
        public T Data { get; set; }
        public string SearchUrl { get; set; }
    }
}