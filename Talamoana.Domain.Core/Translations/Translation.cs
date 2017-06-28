using System.Collections.Generic;
using System.Linq;

namespace Talamoana.Domain.Core.Translations
{
    public sealed class Translation
    {
        public List<string> StatIds { get; }
        public List<TranslationString> Translations { get; }

        public Translation(List<string> ids, List<TranslationString> translations)
        {
            StatIds = ids;
            Translations = translations;
        }
    }
}
