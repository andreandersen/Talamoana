using System.Collections.Generic;

namespace Talamoana.Domain.Core.Stats
{
    public interface StatsDataReader
    {
        IEnumerable<Stat> Read();
    }
}
