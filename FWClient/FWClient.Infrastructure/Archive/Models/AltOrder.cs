using Newtonsoft.Json;

namespace FWClient.Core.Archive
{
    // CollectionList myDeserializedClass = JsonConvert.DeserializeObject<CollectionList>(myJsonResponse);
    public class AltOrder
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("asc")]
        public Asc Asc { get; set; }

        [JsonProperty("desc")]
        public Desc Desc { get; set; }
    }
}