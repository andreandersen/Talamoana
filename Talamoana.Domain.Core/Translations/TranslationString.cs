using System.Collections.Generic;
using System.Linq;

namespace Talamoana.Domain.Core.Translations
{
    public class TranslationString
    {
        public string Translation { get; }
        public IReadOnlyList<TranslationCondition> Conditions { get; }
        public IReadOnlyList<string> Format { get; }
        public IReadOnlyList<IReadOnlyList<string>> Handlers { get; }

        public TranslationString(string translation, IEnumerable<TranslationCondition> condition,
            IEnumerable<string> format, IEnumerable<IEnumerable<string>> handlers)
        {
            Translation = translation;
            Conditions = condition.ToList().AsReadOnly();
            Format = format.ToList().AsReadOnly();
            Handlers = handlers.Select(h => h.ToList().AsReadOnly()).ToList().AsReadOnly();
        }
    }
}
