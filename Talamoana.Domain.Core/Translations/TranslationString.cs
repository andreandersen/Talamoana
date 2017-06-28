using System.Collections.Generic;
using System.Linq;

namespace Talamoana.Domain.Core.Translations
{
    public class TranslationString
    {
        public string Translation { get; }
        public List<TranslationCondition> Conditions { get; }
        public List<string> Format { get; }
        public List<List<string>> Handlers { get; }

        public TranslationString(string translation, List<TranslationCondition> condition,
            List<string> format, List<List<string>> handlers)
        {
            Translation = translation;
            Conditions = condition;
            Format = format;
            Handlers = handlers;
        }
    }
}
