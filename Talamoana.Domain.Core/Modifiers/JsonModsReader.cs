using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Talamoana.Domain.Core.Stats;

namespace Talamoana.Domain.Core.Modifiers
{
    public class JsonModsReader : IModsReader
    {
        private readonly string _file;
        private readonly StatsIndex _statsIndex;

        public JsonModsReader(StatsIndex statsIndex, string sourceFile = "Data\\mods.json")
        {
            _file = sourceFile;
            _statsIndex = statsIndex;
        }

        public JsonModsReader(StatsDataReader statsReader, string sourceFile = "Data\\mods.json")
        {
            _file = sourceFile;
            _statsIndex = new StatsIndex(statsReader);
        }

        public List<Modifier> Read()
        {
            var json = File.ReadAllText(_file);
            var mods = JsonConvert.DeserializeObject<Dictionary<string, ModsObject>>(json);
            return mods.Select(mod =>
            {
                var v = mod.Value;
                
                var stats = v.stats.Select(s =>
                {
                    var stat = _statsIndex[s.id];
                    if (stat != null) return new ModifierStat(stat, s.min, s.max);
                  
                    stat = new Stat(s.id, false, false, new Dictionary<string, string>());

                    return new ModifierStat(stat, s.min, s.max);
                }).ToList();

                var spawnTags = v.spawn_tags.Select(p => new TagWeight(p.id, p.value)).ToList();
                var genWeights = v.generation_weights.Select(p => new TagWeight(p.id, p.value)).ToList();
                
                return new Modifier(mod.Key, v.name, v.domain, v.generation_type, stats, v.adds_tags,
                    v.is_essence_only, v.group, v.required_level, spawnTags, genWeights);
            }).ToList<Modifier>();
        }
        
        #pragma warning disable 649
        // ReSharper disable InconsistentNaming
        // ReSharper disable CollectionNeverUpdated.Local
        // ReSharper disable MemberCanBePrivate.Local
        // ReSharper disable ClassNeverInstantiated.Local
        private class ModsObject
        {
            public List<string> adds_tags;
            public Domain domain;
            public GenerationType generation_type;
            public List<IdValue> generation_weights;
            public string group;
            public bool is_essence_only;
            public string name;
            public int required_level;

            public List<IdValue> spawn_tags;
            public StatsObject[] stats;

            public class IdValue
            {
                public string id;
                public int value;
            }
        }

        private class StatsObject
        {
            public string id;
            public int max;
            public int min;
        }
        // ReSharper restore InconsistentNaming
        // ReSharper restore CollectionNeverUpdated.Local
        // ReSharper restore MemberCanBePrivate.Local        
        // ReSharper restore ClassNeverInstantiated.Local
    }
}