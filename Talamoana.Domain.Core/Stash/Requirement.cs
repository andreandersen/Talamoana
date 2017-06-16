using Newtonsoft.Json;

namespace Talamoana.Domain.Core.Stash
{
    public class Requirement
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("values")]
        public object[][] Values { get; set; }

        [JsonProperty("displayMode")]
        public int DisplayMode { get; set; }
    }
}
