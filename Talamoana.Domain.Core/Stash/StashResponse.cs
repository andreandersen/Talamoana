using Newtonsoft.Json;

namespace Talamoana.Domain.Core.Stash
{
    public class StashResponse
    {
        [JsonProperty("next_change_id")]
        public string NextChangeId { get; private set; }

        [JsonProperty("stashes")]
        public Stash[] Stashes { get; private set; }
    }
}
