using System;
using System.Linq;
using System.Collections.Generic;

namespace Talamoana.Domain.Core.Items
{
    public class RareNames
    {
        public static readonly List<string> Prefixes = new List<string> { "Agony", "Apocalypse", "Armageddon", "Beast", "Behemoth", "Blight", "Blood", "Bramble", "Brimstone", "Brood", "Carrion", "Cataclysm", "Chimeric", "Corpse", "Corruption", "Damnation", "Death", "Demon", "Dire", "Dragon", "Dread", "Doom", "Dusk", "Eagle", "Empyrean", "Fate", "Foe", "Gale", "Ghoul", "Gloom", "Glyph", "Golem", "Grim", "Hate", "Havoc", "Honour", "Horror", "Hypnotic", "Kraken", "Loath", "Maelstrom", "Mind", "Miracle", "Morbid", "Oblivion", "Onslaught", "Pain", "Pandemonium", "Phoenix", "Plague", "Rage", "Rapture", "Rune", "Skull", "Soul", "Sorrow", "Spirit", "Storm", "Tempest", "Torment", "Vengeance", "Victory", "Viper", "Vortex", "Woe", "Wrath" };

        public static readonly Dictionary<string, List<string>> Suffixes = new Dictionary<string, List<string>>
        {
            { "Spirit Shield", new List<string> { "Ancient", "Anthem", "Call", "Chant", "Charm", "Emblem", "Guard", "Pith", "Sanctuary", "Song", "Spell", "Star", "Ward", "Weaver", "Wish" } },
            { "Shields", new List<string> { "Aegis", "Badge", "Barrier", "Bastion", "Bulwark", "Duty", "Emblem", "Fend", "Guard", "Mark", "Refuge", "Rock", "Rook", "Sanctuary", "Span", "Tower", "Watch", "Wing" } },
            { "Body Armours", new List<string> { "Carapace", "Cloak", "Coat", "Curtain", "Guardian", "Hide", "Jack", "Keep", "Mantle", "Pelt", "Salvation", "Sanctuary", "Shell", "Shelter", "Shroud", "Skin", "Suit", "Veil", "Ward", "Wrap" } },
            { "Helmets", new List<string> { "Brow", "Corona", "Cowl", "Crest", "Crown", "Dome", "Glance", "Guardian", "Halo", "Horn", "Keep", "Peak", "Salvation", "Shelter", "Star", "Veil", "Visage", "Visor", "Ward" } },
            { "Gloves", new List<string> { "Caress", "Claw", "Clutches", "Fingers", "Fist", "Grasp", "Grip", "Hand", "Hold", "Knuckle", "Mitts", "Nails", "Palm", "Paw", "Talons", "Touch", "Vise" } },
            { "Boots", new List<string> { "Dash", "Goad", "Hoof", "League", "March", "Pace", "Road", "Slippers", "Sole", "Span", "Spark", "Spur", "Stride", "Track", "Trail", "Tread", "Urge" } },
            { "Amulets", new List<string> { "Beads", "Braid", "Charm", "Choker", "Clasp", "Collar", "Idol", "Gorget", "Heart", "Locket", "Medallion", "Noose", "Pendant", "Rosary", "Scarab", "Talisman", "Torc" } },
            { "Rings", new List<string> { "Band", "Circle", "Coil", "Eye", "Finger", "Grasp", "Grip", "Gyre", "Hold", "Knot", "Knuckle", "Loop", "Nail", "Spiral", "Turn", "Twirl", "Whorl" } },
            { "Belts", new List<string> { "Bind", "Bond", "Buckle", "Clasp", "Cord", "Girdle", "Harness", "Lash", "Leash", "Lock", "Locket", "Shackle", "Snare", "Strap", "Tether", "Thread", "Trap", "Twine" } },
            { "Quivers", new List<string> { "Arrow", "Barb", "Bite", "Bolt", "Brand", "Dart", "Flight", "Hail", "Impaler", "Nails", "Needle", "Quill", "Rod", "Shot", "Skewer", "Spear", "Spike", "Spire", "Stinger" } },
            { "Maps", new List<string> { "Abode", "Bind", "Chambers", "Coffers", "Core", "Cradle", "Cramp", "Crest", "Depths", "Dregs", "Dwelling", "Frontier", "Haven", "Keep", "Oubliette", "Panorama", "Pit", "Point", "Precinct", "Quarters", "Reaches", "Refuge", "Refuse", "Remains", "Roost", "Sanctum", "Scum", "Secrets", "Sepulcher", "Shadows", "Solitude", "Trap", "Vault", "View", "Vine", "Waste", "Ziggurat", "Zone" } },
            { "Axes", new List<string> { "Bane", "Beak", "Bite", "Butcher", "Edge", "Etcher", "Gnash", "Hunger", "Mangler", "Rend", "Roar", "Sever", "Slayer", "Song", "Spawn", "Splitter", "Sunder", "Thirst" } },
            { "Maces", new List<string> { "Bane", "Batter", "Blast", "Blow", "Blunt", "Brand", "Breaker", "Burst", "Crack", "Crusher", "Grinder", "Knell", "Mangler", "Ram", "Roar", "Ruin", "Shatter", "Smasher", "Star", "Thresher", "Wreck" } },
            { "Sceptres", new List<string> { "Bane", "Blow", "Breaker", "Call", "Chant", "Crack", "Crusher", "Cry", "Gnarl", "Grinder", "Knell", "Ram", "Roar", "Smasher", "Song", "Spell", "Star", "Weaver" } },
            { "Staves", new List<string> { "Bane", "Beam", "Branch", "Call", "Chant", "Cry", "Gnarl", "Goad", "Mast", "Pile", "Pillar", "Pole", "Post", "Roar", "Song", "Spell", "Spire", "Weaver" } },
            { "Swords", new List<string> { "Bane", "Barb", "Beak", "Bite", "Edge", "Fang", "Gutter", "Hunger", "Impaler", "Needle", "Razor", "Saw", "Scalpel", "Scratch", "Sever", "Skewer", "Slicer", "Song", "Spike", "Spiker", "Stinger", "Thirst" } },
            { "Daggers", new List<string> { "Bane", "Barb", "Bite", "Edge", "Etcher", "Fang", "Gutter", "Hunger", "Impaler", "Needle", "Razor", "Scalpel", "Scratch", "Sever", "Skewer", "Slicer", "Song", "Spike", "Stinger", "Thirst" } },
            { "Claws", new List<string> { "Bane", "Bite", "Edge", "Fang", "Fist", "Gutter", "Hunger", "Impaler", "Needle", "Razor", "Roar", "Scratch", "Skewer", "Slicer", "Song", "Spike", "Stinger", "Talons", "Thirst" } },
            { "Bows", new List<string> { "Arch", "Bane", "Barrage", "Blast", "Branch", "Breeze", "Fletch", "Guide", "Horn", "Mark", "Nock", "Rain", "Reach", "Siege", "Song", "Stinger", "Strike", "Thirst", "Thunder", "Twine", "Volley", "Wind", "Wing" } },
            { "Wands", new List<string> { "Bane", "Barb", "Bite", "Branch", "Call", "Chant", "Charm", "Cry", "Edge", "Gnarl", "Goad", "Needle", "Scratch", "Song", "Spell", "Spire", "Thirst", "Weaver" } }
        };

        private static readonly Random Rnd = new Random();
        
        public static string GenerateRareName(Item item)
        {            
            List<string> suffixList;
            if (item.Base.Name == "Spirit Shield")
                suffixList = Suffixes["Spirit Shield"];
            else
            {
                var suffixListKey = Suffixes.Keys.FirstOrDefault(p => item.Base.ItemClass.Name.Contains(p));
                suffixList = suffixListKey != null ? 
                    Suffixes[suffixListKey] :
                    // Fallback, i.e. couldn't find rare names for jewels - no priority.    
                    Suffixes.SelectMany(p => p.Value).ToList();
            }

            var prefix = Prefixes[Rnd.Next(Prefixes.Count - 1)];
            var suffix = suffixList[Rnd.Next(suffixList.Count - 1)];

            return $"{prefix} {suffix}";
        }
    }
}