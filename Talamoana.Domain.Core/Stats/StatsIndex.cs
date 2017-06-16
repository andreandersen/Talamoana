using System.Collections.Generic;
using System.Linq;

namespace Talamoana.Domain.Core.Stats
{
    public class StatsIndex
    {
        private readonly Dictionary<string, IStat> _dict;

        public StatsIndex(IStatsDataReader reader)
        {
            _dict = reader.Read()
                .ToDictionary(p => p.Id, p => p);
        }

        public IStat this[string index]
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
