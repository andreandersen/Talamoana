using Jil;
using System.Collections.Generic;

namespace Talamoana.Domain.Core.Stash
{
    public class StashItem
    {
        [JilDirective("w")]
        public int Width { get; set; }

        [JilDirective("h")]
        public int Height { get; set; }

        [JilDirective("ilvl")]
        public int ItemLevel { get; set; }

        [JilDirective("icon")]
        public string IconUrl { get; set; }

        [JilDirective("league")]
        public string League { get; set; }

        [JilDirective("id")]
        public string Id { get; set; }

        [JilDirective("sockets")]
        public List<Socket> Sockets { get; set; }

        private string _name;

        static List<string> nameRemovables = new List<string>() { "<<set:MS>>", "<<set:M>>", "<<set:S>>", "Superior" };

        [JilDirective("name")]
        public string Name
        {
            get => _name;
            set
            {
                string r = value;
                var c = nameRemovables.Count;
                for (int i = 0; i < c; i++)
                {
                    r = r.Replace(nameRemovables[i], "");
                }
                _name = r;
            }
        }

        private string _typeLine;

        [JilDirective("typeLine")]
        public string TypeLine
        {
            get => _typeLine;
            set
            {
                string r = value;
                var c = nameRemovables.Count;
                for (int i = 0; i < c; i++)
                {
                    r = r.Replace(nameRemovables[i], "");
                }
                _typeLine = r;
            }
        }

        [JilDirective("identified")]
        public bool Identified { get; set; }

        [JilDirective("corrupted")]
        public bool Corrupted { get; set; }

        [JilDirective("lockedToCharacter")]
        public bool LockedToCharacter { get; set; }

        [JilDirective("properties")]
        public List<Property> Properties { get; set; }

        [JilDirective("requirements")]
        public List<Requirement> Requirements { get; set; }

        [JilDirective("frameType")]
        public int FrameType { get; set; }

        [JilDirective("x")]
        public int Left { get; set; }

        [JilDirective("y")]
        public int Top { get; set; }

        [JilDirective("inventoryId")]
        public string InventoryId { get; set; }

        [JilDirective("utilityMods")]
        public List<string> UtilityMods { get; set; }

        [JilDirective("implicitMods")]
        public List<string> ImplicitMods { get; set; }

        [JilDirective("explicitMods")]
        public List<string> ExplicitMods { get; set; }

        [JilDirective("enchantMods")]
        public List<string> EnchantMods { get; set; }

        [JilDirective("craftedMods")]
        public List<string> CraftedMods { get; set; }

        [JilDirective("cosmeticMods")]
        public List<string> CosmeticMods { get; set; }

        [JilDirective("note")]
        public string Note { get; set; }

        [JilDirective("stackSize")]
        public int? StackSize { get; set; }

        [JilDirective("maxStackSize")]
        public int? MaxStackSize { get; set; }

        [JilDirective("talismanTier")]
        public int? TalismanTier { get; set; }

        [JilDirective("artFilename")]
        public string ArtFilename { get; set; }

        [JilDirective("isRelic")]
        public bool IsRelic { get; set; }

        [JilDirective("support")]
        public bool IsSupport { get; set; }
    }
}
