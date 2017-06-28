using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Talamoana.Domain.Core.Stats
{
    public class JsonStatsReader : StatsDataReader
    {
        private readonly string _file;

        public JsonStatsReader(string sourceFile = "Data\\stats.json")
        {
            _file = sourceFile;
        }

        public IEnumerable<Stat> Read()
        {
            var json = File.ReadAllText(_file);
            var objects = JsonConvert.DeserializeObject<Dictionary<string, StatObject>>(json);

            foreach (var item in objects)
            {                
                yield return new Stat(item.Key, item.Value.is_local,
                    item.Value.is_aliased, item.Value.alias);
            }
        }

        #pragma warning disable 649
        // ReSharper disable InconsistentNaming
        // ReSharper disable once ClassNeverInstantiated.Local
        private class StatObject
        {
            public Dictionary<string, string> alias;
            public bool is_aliased;
            public bool is_local;
        }
        // ReSharper restore InconsistentNaming
    }
}