using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Talamoana.Domain.Core.Items.Crafting.Actions;
using Talamoana.Domain.Core.Modifiers;
using Talamoana.Domain.Core.Shared;
using Talamoana.Domain.Core.Stats;

namespace Talamoana.Domain.Core.Items.Crafting.Strategies
{
    public class AltCraftingStrategy : ICraftingStrategy
    {
        private readonly Dictionary<Type, IRandomCraftingAction> _actions;
        private readonly IReadOnlyList<IModifier> _allMods;
        private int _totalRolls = 0;
        private Stopwatch _stopwatch;
        
        public AltCraftingStrategy(IReadOnlyList<IModifier> allMods, IRandomizer randomizer = null)
        {
            var random = randomizer ?? new PseudoRandom();
            _actions = Util.DefaultRandomCraftingActions(allMods, random);

            _allMods = allMods;
        }

        /// <inheritdoc />
        public IReadOnlyDictionary<Type, int> Execute(Item item, IReadOnlyDictionary<string, IReadOnlyDictionary<IStat, int>> desiredModGroupValues, CancellationToken ct)
        {
            // Determine if this is possible based on the mod pool and the weights
            var rollableMods = _allMods.GetRollableExplicits(item);
            var isPossible = desiredModGroupValues.All(modGroup =>
            {
                return rollableMods.Any(rm => rm.Group == modGroup.Key && rm.Stats.All(s => modGroup.Value[s.Stat] <= s.Max));
            });

            if (!isPossible)
                throw new InvalidOperationException("Can't do it");
            _stopwatch = Stopwatch.StartNew();
            var cost = Util.CreateCostDictionary(_actions);
            PrepItem(item, cost);
            AttemptReachGoal(item, cost, desiredModGroupValues, ct);

            PrintStatus(item, cost, desiredModGroupValues, true);
            return cost;
        }

        private void AttemptReachGoal(Item item, Dictionary<Type, int> cost,
            IReadOnlyDictionary<string, IReadOnlyDictionary<IStat, int>> desiredModGroupValues, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                if (item.Rarity == ItemRarity.Rare)
                    Roll<ScourOrb>();

                if (item.Rarity == ItemRarity.Normal)
                    Roll<TransmutationOrb>();

                if (item.AllDesiredSatisfied(desiredModGroupValues)) break;
                if (!item.SoFarSoGood(desiredModGroupValues))
                    Roll<AlterationOrb>();

                if (item.AllDesiredSatisfied(desiredModGroupValues)) break;
                if (!item.SoFarSoGood(desiredModGroupValues)) continue;
                if (item.Explicits.Count == 1)
                    Roll<AugmentationOrb>();

                if (item.AllDesiredSatisfied(desiredModGroupValues)) break;
                if (!item.SoFarSoGood(desiredModGroupValues)) continue;

                Roll<RegalOrb>();

                if (item.AllDesiredSatisfied(desiredModGroupValues)) break;
                if (!item.SoFarSoGood(desiredModGroupValues)) continue;

                while (item.Explicits.Count < Math.Min(6, desiredModGroupValues.Count))
                {
                    Roll<ExaltedOrb>();

                    if (!item.SoFarSoGood(desiredModGroupValues) ||
                        item.AllDesiredSatisfied(desiredModGroupValues)) break;
                }

                if (item.AllDesiredSatisfied(desiredModGroupValues)) break;
            }

            void Roll<T>()
            {
                _totalRolls++;
                _actions.Apply<T>(item);
                cost.Increment<T>();

                PrintStatus(item, cost, desiredModGroupValues);
            }
        }

        private void PrepItem(Item item, Dictionary<Type, int> cost)
        {
            if (item.Rarity == ItemRarity.Magic) return;

            _actions
                .Apply<ScourOrb>(item)
                .Apply<TransmutationOrb>(item);

            cost.Increment<ScourOrb>()
                .Increment<TransmutationOrb>();
        }

        private int _i;
        private void PrintStatus(Item item, Dictionary<Type, int> cost,
            IReadOnlyDictionary<string, IReadOnlyDictionary<IStat, int>> desiredModGroupValues, bool force = false)
        {
            return;
            if (++_i % 100 != 0 && !force) return;
            _i = 0;

            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            cost.ToList().ForEach(c => Console.WriteLine($"{c.Key.Name,-30} {c.Value:###,###}".PadRight(Console.WindowWidth - 4)));

            Console.SetCursorPosition(0, 10);

            var col1 = desiredModGroupValues.OrderBy(c => c.Key)
                .Select(c => $"{c.Key,-25} ({string.Join(",", c.Value.Select(e => e.Value))})").ToList();

            var col2 = item.Explicits.OrderBy(c => c.Modifier.Group).Select(c => $"{c.Modifier.Id,-25} ({string.Join(",", c.Values.Select(e => e.Value))})").ToList();

            col1.Insert(0, "--------------------------------------");
            col2.Insert(0, "--------------------------------------");

            col1.Insert(0, "Desired result");
            col2.Insert(0, "Outcome");

            var p =
                from idx in Enumerable.Range(0, 6)
                let c1 = col1.ElementAtOrDefault(idx) ?? ""
                let c2 = col2.ElementAtOrDefault(idx) ?? ""
                select $"{c1,-40} {c2}".PadRight(Console.WindowWidth - 1);

            Console.WriteLine(string.Join(Environment.NewLine, p));

            Console.WriteLine(
                $"\r\n\r\nRolls per second: {(_totalRolls / _stopwatch.Elapsed.TotalSeconds),-10:###,###}");
        }
    }
}