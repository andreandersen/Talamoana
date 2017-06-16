using System.Linq;
using Talamoana.Domain.Core.Stats;
using Talamoana.Domain.Core.Translations;
using Xunit;

namespace Talamoana.Tests.Domain.Core
{
    public class StatsReadTest
    {
        [Fact]
        public void CanCreateAndRead()
        {
            var index = new StatsIndex(new JsonStatsReader());
            Assert.NotEqual(0, index.Count);
            var found = index["base_number_of_additional_arrows"];
            Assert.NotNull(found);
        }
    }
}
