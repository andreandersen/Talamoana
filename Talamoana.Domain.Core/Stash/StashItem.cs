using Newtonsoft.Json;

namespace Talamoana.Domain.Core.Stash
{
    public class StashItem
    {
        [JsonProperty("w")]
        public int Width { get; set; }

        [JsonProperty("h")]
        public int Height { get; set; }

        [JsonProperty("ilvl")]
        public int ItemLevel { get; set; }

        [JsonProperty("icon")]
        public string IconUrl { get; set; }

        [JsonProperty("league")]
        public string League { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sockets")]
        public Socket[] Sockets { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("typeLine")]
        public string TypeLine { get; set; }

        [JsonProperty("identified")]
        public bool Identified { get; set; }

        [JsonProperty("corrupted")]
        public bool Corrupted { get; set; }

        [JsonProperty("lockedToCharacter")]
        public bool LockedToCharacter { get; set; }

        [JsonProperty("properties")]
        public Property[] Properties { get; set; }

        [JsonProperty("requirements")]
        public Requirement[] Requirements { get; set; }

        [JsonProperty("frameType")]
        public int FrameType { get; set; }

        [JsonProperty("x")]
        public int Left { get; set; }

        [JsonProperty("y")]
        public int Top { get; set; }

        [JsonProperty("inventoryId")]
        public string InventoryId { get; set; }

        [JsonProperty("utilityMods")]
        public string[] UtilityMods { get; set; }

        [JsonProperty("implicitMods")]
        public string[] ImplicitMods { get; set; }

        [JsonProperty("explicitMods")]
        public string[] ExplicitMods { get; set; }

        [JsonProperty("enchantMods")]
        public string[] EnchantMods { get; set; }

        [JsonProperty("craftedMods")]
        public string[] CraftedMods { get; set; }

        [JsonProperty("cosmeticMods")]
        public string[] CosmeticMods { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("stackSize")]
        public int? StackSize { get; set; }

        [JsonProperty("maxStackSize")]
        public int? MaxStackSize { get; set; }

        [JsonProperty("talismanTier")]
        public int? TalismanTier { get; set; }

        [JsonProperty("artFilename")]
        public string ArtFilename { get; set; }

        [JsonProperty("isRelic")]
        public bool IsRelic { get; set; }

        [JsonProperty("support")]
        public bool IsSupport { get; set; }
    }
}
