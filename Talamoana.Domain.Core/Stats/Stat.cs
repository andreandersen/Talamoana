using System.Collections.Generic;
using Talamoana.Domain.Core.Translations;

namespace Talamoana.Domain.Core.Stats
{
    public class Stat : IStat
    {
        public string Id { get; }
        public bool IsLocal { get; }
        public bool IsAliased { get; }
        public IReadOnlyDictionary<string, string> Aliases { get; }

        public Stat(string id, bool isLocal, bool isAliased, 
            IReadOnlyDictionary<string, string> aliases)
        {
            Id = id;
            IsLocal = isLocal;
            IsAliased = isAliased;
            Aliases = aliases;
        }
    }
}
