using System;
using System.Collections.Generic;
using System.Linq;
using Talamoana.Domain.Core.Items.Crafting.Actions;
using Talamoana.Domain.Core.Modifiers;
using Talamoana.Domain.Core.Shared;
using Talamoana.Domain.Core.Stats;

namespace Talamoana.Domain.Core.Items.Crafting.Strategies
{
    public static class Util
    {
        public static bool AllDesiredSatisfied(this Item item,
            IReadOnlyDictionary<string, IReadOnlyDictionary<IStat, int>> desiredModGroupValues) =>
            desiredModGroupValues.All(d =>
            {
                var itemModMatch = item.Explicits.FirstOrDefault(c => c.Modifier.Group == d.Key);
                return itemModMatch != null && itemModMatch.Values.All(c => d.Value[c.Key] <= c.Value);
            });

        public static bool SoFarSoGood(this Item item,
            IReadOnlyDictionary<string, IReadOnlyDictionary<IStat, int>> desiredModGroupValues) =>
            item.Explicits.All(e => desiredModGroupValues.TryGetValue(e.Modifier.Group, out var desired) &&
                                    desired.All(dv => e.Values[dv.Key] > dv.Value));


        public static Dictionary<Type, IRandomCraftingAction> Apply<T>(
            this Dictionary<Type, IRandomCraftingAction> actions, Item item)
        {
            actions[typeof(T)].Apply(item);
            return actions;
        }

        public static Dictionary<Type, int> CreateCostDictionary(Dictionary<Type, IRandomCraftingAction> actions) =>
            actions.ToDictionary(p => p.Key, _ => 0);

        public static Dictionary<Type, int> Increment<T>(this Dictionary<Type, int> cost)
        {
            if (!cost.ContainsKey(typeof(T)))
                cost.Add(typeof(T), 1);
            else
                cost[typeof(T)] = cost[typeof(T)] + 1;

            return cost;
        }

        public static Dictionary<Type, IRandomCraftingAction> DefaultRandomCraftingActions(
            IReadOnlyList<IModifier> allMods,
            IRandomizer random) =>
            new List<IRandomCraftingAction>
            {
                new AlchemyOrb(random, allMods),
                new AlterationOrb(random, allMods),
                new AugmentationOrb(allMods),
                new ChaosOrb(random, allMods),
                new ExaltedOrb(random, allMods),
                new RegalOrb(random, allMods),
                new TransmutationOrb(random, allMods),
                new ScourOrb()
            }.ToDictionary(p => p.GetType(), p => p);
    }
}