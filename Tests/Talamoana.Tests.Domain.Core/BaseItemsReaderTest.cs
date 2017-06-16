using System;
using System.Collections.Generic;
using System.Linq;
using Talamoana.Domain.Core.Items;
using Talamoana.Domain.Core.Modifiers;
using Talamoana.Domain.Core.Stats;
using Xunit;

namespace Talamoana.Tests.Domain.Core
{
    public class BaseItemsReaderTest
    {
        
        [Fact]
        public void CanCreateAndRead()
        {
            var statsReader = new JsonStatsReader();
            var statsIndex = new StatsIndex(statsReader);
            var modsReader = new JsonModsReader(statsIndex);
            var mods = modsReader.Read().ToList();

            var baseItemsReader = new JsonItemReader(mods);
            var stuff = baseItemsReader.Read().ToList();
        }
    }
}
