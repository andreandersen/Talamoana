using System.Collections.Generic;
using Talamoana.Domain.Core.Translations;

namespace Talamoana.Domain.Core.Stats
{
    public interface IStat
    {
        string Id { get; }
        bool IsAliased { get; }
        IReadOnlyDictionary<string, string> Aliases { get; }
        bool IsLocal { get; }
    }
}
