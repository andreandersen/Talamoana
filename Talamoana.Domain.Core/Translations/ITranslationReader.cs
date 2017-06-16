using System.Collections.Generic;

namespace Talamoana.Domain.Core.Translations
{
    /// <summary>
    /// Read translation items from a source into domain model.
    /// The constructor should take source parameters, meanwhile
    /// the Read method is parameterless.
    /// </summary>
    public interface ITranslationReader
    {
        IEnumerable<Translation> Read();
    }
}
