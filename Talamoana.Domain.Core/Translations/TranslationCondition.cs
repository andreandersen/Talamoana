// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace Talamoana.Domain.Core.Translations
{
    /// <summary>
    /// Numeric range to where the translation variation will be applicable.
    /// </summary>
    public class TranslationCondition
    {
        public int? Min { get; }
        public int? Max { get; }

        public TranslationCondition(int? min, int? max)
        {
            Min = min;
            Max = max;
        }
    }
}
