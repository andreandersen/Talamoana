using Newtonsoft.Json;

namespace Talamoana.Domain.Core.Stash
{
    public class Stash
    {
        [JsonProperty("accountName")]
        public string AccountName { get; set; }

        [JsonProperty("lastCharacterName")]
        public string LastCharacterName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("Stash")]
        public string StashName { get; set; }

        [JsonProperty("stashType")]
        public string StashType { get; set; }

        [JsonProperty("public")]
        public bool IsPublic { get; set; }

        [JsonProperty("items")]
        public StashItem[] Items { get; set; }
    }
}
