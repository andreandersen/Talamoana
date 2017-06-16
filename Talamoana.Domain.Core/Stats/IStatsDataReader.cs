using System.Collections.Generic;

namespace Talamoana.Domain.Core.Stats
{
    public interface IStatsDataReader
    {
        IEnumerable<IStat> Read();
    }
}
