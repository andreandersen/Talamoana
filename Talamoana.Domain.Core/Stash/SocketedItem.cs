using Newtonsoft.Json;

namespace Talamoana.Domain.Core.Stash
{
    public class SocketedItem
    {
        [JsonProperty("ilvl")]
        public int ItemLevel { get; set; }

        [JsonProperty("icon")]
        public string IconUrl { get; set; }

        [JsonProperty("support")]
        public bool IsSupport { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("typeLine")]
        public string TypeLine { get; set; }

        [JsonProperty("corrupted")]
        public bool IsCorrupted { get; set; }

        [JsonProperty("locketToCharacter")]
        public bool IsLockedToCharacter { get; set; }

        [JsonProperty("properties")]
        public Property[] Properties { get; set; }

        [JsonProperty("additionalProperties")]
        public AdditionalProperty[] AdditionalProperties { get; set; }

        [JsonProperty("requirements")]
        public Requirement[] Requirements { get; set; }

        [JsonProperty("explicitMods")]
        public string[] ExplicitMods { get; set; }

        [JsonProperty("socket")]
        public int Socket { get; set; }

        [JsonProperty("colour")]
        public string Colour { get; set; }
    }
}
