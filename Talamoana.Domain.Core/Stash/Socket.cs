using Newtonsoft.Json;

namespace Talamoana.Domain.Core.Stash
{
    public class Socket
    {
        [JsonProperty("group")]
        public int Group { get; set; }

        [JsonProperty("attr")]
        public string Attribute { get; set; }
    }
}
