using System;
using System.Diagnostics;
using System.Linq;
using Talamoana.Domain.Core.Items;
using Talamoana.Domain.Core.Modifiers;
using Talamoana.Domain.Core.Stats;
using Talamoana.Domain.Core.Translations;
using Xunit;

namespace Talamoana.Tests.Domain.Core
{
    public class ModsReadTest
    {
        [Fact]
        public void CanCreateAndRead()
        {
            var mods = new JsonModsReader(new JsonStatsReader()).Read().ToList();
            var baseItems = new JsonItemReader(mods).Read().ToList();
            var baseItem = baseItems.First(p => p.Name == "Steel Ring");

            var item = new Item(baseItem, 84);
        }

    }
}
