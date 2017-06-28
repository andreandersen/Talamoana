using System.Collections.Generic;
using System.Linq;

namespace Talamoana.Domain.Core.Stats
{
    public class StatsIndex
    {
        private readonly Dictionary<string, Stat> _dict;

        public StatsIndex(StatsDataReader reader)
        {
            _dict = reader.Read()
                .ToDictionary(p => p.Id, p => p);
        }

        public Stat this[string index]
        {
            get
            {
                _dict.TryGetValue(index, out var value);
                return value;
            }
        }

        public long Count => _dict.Count;
    }
}
