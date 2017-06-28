using System.Collections.Generic;

namespace Talamoana.Domain.Core.Modifiers
{
    public interface IModsReader
    {
        List<Modifier> Read();
    }
}
