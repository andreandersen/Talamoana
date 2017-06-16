using System.IO;
using Talamoana.Domain.Core.Stash;
using Xunit;

namespace Talamoana.Tests.Domain.Core
{
    public class StashReaderTests
    {
        [Fact]
        public void TestDeserializationOfStashItems()
        {
            var json = File.ReadAllText("Data\\Test-Stashes\\test01.json");
            var stashResponse = StashDeserializer.DeserializeResponse(json);
        }
    }
}
