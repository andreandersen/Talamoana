using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Talamoana.Domain.Core.Stats;

namespace Talamoana.Domain.Core.Translations
{
    public class TranslatedStat
    {
        public Stat Stat { get; }
        public string HumanReadableStat { get; }
        public Translation Translation { get; }
        public int Min { get; }
        public int Max { get; }

        public TranslatedStat(Stat stat, string humanReadable, Translation translation,
            int min = 0, int max = 0)
        {
            Stat = stat;
            HumanReadableStat = humanReadable;
            Translation = translation;
            Min = min;
            Max = max;
        }
    }
}
