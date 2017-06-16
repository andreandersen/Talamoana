using System;
using System.Collections.Generic;
using System.Threading;
using Talamoana.Domain.Core.Stats;

namespace Talamoana.Domain.Core.Items.Crafting.Strategies
{
    public interface ICraftingStrategy
    {
        IReadOnlyDictionary<Type, int> Execute(Item item, IReadOnlyDictionary<string, IReadOnlyDictionary<IStat, int>> desiredModGroupValues, CancellationToken ct);
    }
}
