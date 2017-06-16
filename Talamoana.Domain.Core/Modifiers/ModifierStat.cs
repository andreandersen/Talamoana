using Talamoana.Domain.Core.Stats;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace Talamoana.Domain.Core.Modifiers
{
    public class ModifierStat
    {
        public ModifierStat(IStat stat, int min, int max)
        {
            Stat = stat;
            Min = min;
            Max = max;
        }

        public IStat Stat { get; }
        public int Min { get; }
        public int Max { get; }
    }
}