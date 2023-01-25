using FWClient.Core.Albums;
using FWClient.Core.Common;
using Newtonsoft.Json;

namespace FWClient.Core.Application
{
    public class FwClient : IFwClient
    {
        private readonly HttpClient _cleint;

        public FwClient(HttpClient cleint)
        {
            _cleint = cleint ?? throw new ArgumentNullException(nameof(cleint));
        }

        public async Task<List<Album>> GetMyAlbums()
        {
            var httpResponseMessage = await _cleint.GetAsync("fotoweb/me/albums/");

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Request exception");
            }

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            var albums = JsonConvert.DeserializeObject<ResponseResult<List<Album>>>(content); ;

            return albums!.Data;
        }
    }
}
