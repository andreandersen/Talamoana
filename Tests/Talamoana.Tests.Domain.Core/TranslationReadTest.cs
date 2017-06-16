using Talamoana.Domain.Core.Translations;
using Xunit;

namespace Talamoana.Tests.Domain.Core
{
    public class TranslationReadTest
    {
        [Fact]
        public void CanReadTranslations()
        {
            var t = new JsonTranslationsReader();
            var index = new TranslationsIndex(t);
        }
    }
}
