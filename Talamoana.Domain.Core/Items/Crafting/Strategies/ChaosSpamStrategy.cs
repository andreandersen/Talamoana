using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Talamoana.Domain.Core.Items.Crafting.Actions;
using Talamoana.Domain.Core.Modifiers;
using Talamoana.Domain.Core.Shared;
using Talamoana.Domain.Core.Stats;

namespace Talamoana.Domain.Core.Items.Crafting.Strategies
{
    public class ChaosSpamStrategy : ICraftingStrategy
    {
        private readonly Dictionary<Type, IRandomCraftingAction> _actions;
        private readonly List<Modifier> _allMods;
        private int _totalRolls = 0;
        private Stopwatch _stopwatch;

        public ChaosSpamStrategy(List<Modifier> allMods, IRandomizer randomizer = null)
        {
            var random = randomizer ?? new PseudoRandom();
            _actions = Util.DefaultRandomCraftingActions(allMods, random);

            _allMods = allMods;
        }

        /// <inheritdoc />
        public Dictionary<Type, int> Execute(Item item, Dictionary<string, Dictionary<Stat, int>> desiredMods, CancellationToken ct)
        {
            var rollableMods = _allMods.GetRollableExplicits(item);
            var isPossible = desiredMods.All(modGroup =>
            {
                return rollableMods.Any(rm => rm.Group == modGroup.Key &&
                                              rm.Stats.All(s => modGroup.Value[s.Stat] <= s.Max));
            });

            if (!isPossible)
                throw new InvalidOperationException("Can't do it");

            _stopwatch = Stopwatch.StartNew();
            var cost = Util.CreateCostDictionary(_actions);
            PrepItem(item, cost);
            AttemptReachGoal(item, cost, desiredMods, ct);
            OnGoalReach?.Invoke(this, new RolledEventArgs(cost, item, _stopwatch.Elapsed));
            return cost;
        }

        private void AttemptReachGoal(Item item, Dictionary<Type, int> cost, Dictionary<string, Dictionary<Stat, int>> desiredModGroupValues, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                Roll<ChaosOrb>();

                if (!IsSatisfactory()) continue;
                if (item.Explicits.Count >= Math.Min(desiredModGroupValues.Count, 6))
                    break;

                while (item.Explicits.Count < Math.Min(5, desiredModGroupValues.Count) && IsSatisfactory())
                {
                    Roll<ExaltedOrb>();
                }

                break;
            }

            bool IsSatisfactory()
            {

                if (desiredModGroupValues.Count > item.Explicits.Count)
                {
                    for (int i = 0; i < item.Explicits.Count; i++)
                    {
                        var im = item.Explicits[i];
                        if (desiredModGroupValues.TryGetValue(im.Modifier.Group, out var dm))
                        {
                            foreach (var d in dm)
                            {
                                if (im.Values[d.Key] < d.Value)
                                    return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else return true;
            }

            void Roll<T>()
            {
                _totalRolls++;
                _actions.Apply<T>(item);
                cost.Increment<T>();
                OnRolled?.Invoke(this, new RolledEventArgs(cost, item, _stopwatch.Elapsed));
            }
        }

        private void PrepItem(Item item, Dictionary<Type, int> cost)
        {
            if (item.Rarity == ItemRarity.Normal)
            {
                _actions.Apply<RegalOrb>(item);
                cost.Increment<RegalOrb>();
            }
            else if (item.Rarity == ItemRarity.Normal)
            {
                _actions.Apply<AlchemyOrb>(item);
                cost.Increment<AlchemyOrb>();
            }
        }

        public event EventHandler<RolledEventArgs> OnRolled;
        public event EventHandler<RolledEventArgs> OnGoalReach;
    }
}
