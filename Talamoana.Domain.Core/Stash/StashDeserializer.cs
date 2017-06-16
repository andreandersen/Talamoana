using Newtonsoft.Json;

namespace Talamoana.Domain.Core.Stash
{
    public static class StashDeserializer
    {
        public static StashResponse DeserializeResponse(string content) => 
            JsonConvert.DeserializeObject<StashResponse>(content);
    }
}
