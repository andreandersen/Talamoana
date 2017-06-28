using System;
using System.Collections.Generic;
using System.Linq;
using Talamoana.Domain.Core.Modifiers;

namespace Talamoana.Domain.Core.Translations
{
    public class TranslationsIndex
    {
        public List<Translation> Translations { get; }

        public TranslationsIndex(ITranslationReader reader)
        {
            Translations = reader.Read().ToList();
        }

        public IEnumerable<TranslatedStat> TranslateModifier(Modifier modifier)
        {
            var stats = modifier.Stats.ToList();

            while (stats.Count > 0)
            {
                var stat = stats[0];

                var found = Translations.Where(c => c.StatIds.Contains(stat.Stat.Id)).ToList();
                if (found.Count > 1)
                    throw new Exception("More than one found, not sure what to do");

                if (found.Count == 0)
                {
                    stats.Remove(stat);
                    continue;
                }

                var firstFound = found.First();

                var translationString = firstFound.Translations.Where(c => c.Conditions.All(e => ConditionSatisfiesRange(e, stat))).First();

                var statsInTranslation = modifier.Stats.Where(c => firstFound.StatIds.Contains(c.Stat.Id)).ToList();

                var str = Convert(translationString, statsInTranslation);


                stats.RemoveAll(c => firstFound.StatIds.Contains(c.Stat.Id));

                if (string.IsNullOrEmpty(str))
                    continue;
                    
                yield return new TranslatedStat(stat.Stat, str, firstFound, stat.Min, stat.Max);
            }
        }

        public static bool ConditionSatisfiesRange(TranslationCondition condition, ModifierStat modifierStat) =>
            (!condition.Max.HasValue || condition.Max >= modifierStat.Max) &&
            (!condition.Min.HasValue || condition.Min <= modifierStat.Min);

        public static string Convert(TranslationString translationString, List<ModifierStat> ranges)
        {
            var str = translationString.Translation;
            for (int i = 0; i < translationString.Format.Count; i++)
            {
                decimal min = ranges[i].Min;
                decimal max = ranges[i].Max;

                translationString.Handlers[i].ForEach(h =>
                {
                    min = TranslationHandlers.Handlers[h].Handle(min);
                    max = TranslationHandlers.Handlers[h].Handle(max);
                });

                if (ranges[i].Max != ranges[i].Min)
                    str = str.Replace($"{{{i}}}", $"({min}-{max})");
                else
                    str = str.Replace($"{{{i}}}", max.ToString());
            }
            return str;
        }

    }
}