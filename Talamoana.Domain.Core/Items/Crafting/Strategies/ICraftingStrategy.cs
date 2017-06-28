using System;
using System.Collections.Generic;
using System.Threading;
using Talamoana.Domain.Core.Stats;

namespace Talamoana.Domain.Core.Items.Crafting.Strategies
{
    public interface ICraftingStrategy
    {
        Dictionary<Type, int> Execute(Item item, Dictionary<string, Dictionary<Stat, int>> desiredModGroupValues, CancellationToken ct);

        event EventHandler<RolledEventArgs> OnRolled;
        event EventHandler<RolledEventArgs> OnGoalReach;
    }
}
