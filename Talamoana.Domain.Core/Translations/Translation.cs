using System.Collections.Generic;
using System.Linq;

namespace Talamoana.Domain.Core.Translations
{
    public sealed class Translation
    {
        public IReadOnlyList<string> StatIds { get; }
        public IReadOnlyList<TranslationString> Translations { get; }

        public Translation(IEnumerable<string> ids, IEnumerable<TranslationString> translations)
        {
            StatIds = ids.ToList().AsReadOnly();
            Translations = translations.ToList().AsReadOnly();
        }
    }
}
