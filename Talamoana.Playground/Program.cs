using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Talamoana.Domain.Core.Items;
using Talamoana.Domain.Core.Items.Crafting.Strategies;
using Talamoana.Domain.Core.Modifiers;
using Talamoana.Domain.Core.Shared;
using Talamoana.Domain.Core.Stats;

namespace Talamoana.Playground
{
    internal class Program
    {
        static int total = 100;
        static int completed = 0;
        static int concurrency = Environment.ProcessorCount;
        static Stopwatch sw;
        static Stopwatch sw_print;

        private static void Do(int x, ConcurrentDictionary<int, Dictionary<Type, int>> cost,
            ICraftingStrategy strategy, Item item, Dictionary<string, Dictionary<Stat, int>> desiredMods,
            CancellationToken ct)
        {
            strategy.OnGoalReach += (s, e) =>
            {
                cost.AddOrUpdate(x, e.TotalRolled, (_, __) => e.TotalRolled);
                PrintStatus(item, cost, desiredMods);
            };

            strategy.OnRolled += (s, e) =>
            {
                cost.AddOrUpdate(x, e.TotalRolled, (_, __) => e.TotalRolled);
                PrintStatus(item, cost, desiredMods);
            };

            strategy.Execute(item, desiredMods, ct);
            PrintStatus(item, cost, desiredMods, true);
        }
        private static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;
            if (Console.WindowHeight < 50)
                Console.WindowHeight = 50;

            Prepare(out var allMods, out var baseItem, out var desiredModifiers);

            var costResults = new ConcurrentDictionary<int, Dictionary<Type, int>>();

            sw = Stopwatch.StartNew();
            sw_print = Stopwatch.StartNew();

            var x = 0;

            Parallel.For(0, total,
                new ParallelOptions { MaxDegreeOfParallelism = concurrency },
                i =>
                {
                    costResults.AddOrUpdate(x, new Dictionary<Type, int>(), (_, __) => new Dictionary<Type, int>());
                    var strategy = new AltCraftingStrategy(allMods, new CryptoRandom());
                    var itemx = new Item(baseItem, 84, ItemRarity.Rare);
                    Do(x++, costResults, strategy, itemx, desiredModifiers, CancellationToken.None);
                    completed++;
                });

            if (Debugger.IsAttached)
            {
                Console.WriteLine("\r\n\r\nPress any key to exit...");
                Console.ReadKey(true);
            }
        }

        private static void Prepare(out List<Modifier> allMods, out Domain.Core.Items.Base.BaseItem
            baseItem, out Dictionary<string, Dictionary<Stat, int>> desiredModifiers)
        {
            allMods = new JsonModsReader(new JsonStatsReader()).Read()
                .Where(p => p.Domain == Domain.Core.Modifiers.Domain.Item &&
                            (p.GenerationType == GenerationType.Prefix ||
                             p.GenerationType == GenerationType.Suffix)).ToList();
            var baseItems = new JsonItemReader(allMods).Read().ToList();
            baseItem = baseItems.First(p => p.Name == "Steel Ring");
            allMods = allMods.GetRollableExplicits(new Item(baseItem, 100) { Rarity = ItemRarity.Magic }).ToList();

            var lifeMod = allMods.First(p => p.Id == "IncreasedLife7"); // Prefix 70 - 79
            var physMod = allMods.First(p => p.Id == "AddedPhysicalDamage6"); // Prefix 9 - 15
            var evaMod = allMods.First(p => p.Id == "IncreasedEvasionRating7"); // Prefix 151 - 170

            var allResMod = allMods.First(p => p.Id == "AllResistances5"); // Suffix 13 - 16
            var atkSpdMod = allMods.First(p => p.Id == "IncreasedAttackSpeed1"); // Suffix 5 - 7
            var chaosResMod = allMods.First(p => p.Id == "ChaosResist6"); // Suffix 31 - 35
            var coldResist = allMods.First(p => p.Id == "ColdResist6");
            var fireResist = allMods.First(p => p.Id == "FireResist6");
            var lightningResist = allMods.First(p => p.Id == "LightningResist6");

            desiredModifiers = new List<MaterializedModifier>
            {
                lifeMod.Materialize(new Dictionary<Stat, int> { { lifeMod.Stats[0].Stat, 70 } }),
                physMod.Materialize(new Dictionary<Stat, int> { { physMod.Stats[0].Stat, 6 }, { physMod.Stats[1].Stat, 11 } }),
                evaMod.Materialize(new Dictionary<Stat, int> { { evaMod.Stats[0].Stat, 5 } }),
                allResMod.Materialize(new Dictionary<Stat, int> { { allResMod.Stats[0].Stat, 12 } }),
                atkSpdMod.Materialize(new Dictionary<Stat, int> { { atkSpdMod.Stats[0].Stat, 6 } }),
                chaosResMod.Materialize(new Dictionary<Stat, int> { { chaosResMod.Stats[0].Stat, 25 } }),
                coldResist.Materialize(new Dictionary<Stat, int> { { coldResist.Stats[0].Stat, 38 } }),
                fireResist.Materialize(new Dictionary<Stat, int> { { fireResist.Stats[0].Stat, 38 } }),
                lightningResist.Materialize(new Dictionary<Stat, int> { { lightningResist.Stats[0].Stat, 38 } }),
            }.ToDictionary(
                p => p.Modifier.Group,
                p => p.Values.ToDictionary(c => c.Key, c => c.Value));
        }

        static void PrintStatus(Item item, ConcurrentDictionary<int, Dictionary<Type, int>> cost,
            Dictionary<string, Dictionary<Stat, int>> desiredModGroupValues, bool force = false)
        {
            if (sw_print.ElapsedMilliseconds < 250 && !force) return;

            lock (Talamoana.Util.ConsoleLock)
            {
                Console.SetCursorPosition(0, 0);

                Console.WriteLine($"{completed}/{total} simulations completed. Concurrency: {concurrency}");

                lock (cost)
                {
                    var totalRolls = cost.SelectMany(c => c.Value)
                        .ToList()
                        .GroupBy(c => c.Key, c => c.Value)
                        .ToDictionary(c => c.Key, c => (int)c.Sum())
                        .ToList();

                    var avgRolls = cost.SelectMany(c => c.Value)
                        .ToList()
                        .GroupBy(c => c.Key, c => c.Value)
                        .ToDictionary(c => c.Key, c => (int)c.Average())
                        .ToList();


                    Console.WriteLine($"Rolls per second: {(totalRolls.Sum(c => c.Value) / sw.Elapsed.TotalSeconds),-10:###,###}\r\n\r\n" +
                        $"Average cost per item:\r\n");


                    avgRolls.ForEach(c => Console.WriteLine($"{c.Key.Name,-30} {c.Value}".PadRight(Console.WindowWidth - 4)));
                    Console.WriteLine("\r\n");

                }

                sw_print.Restart();
                if (!force) return;

                var col1 = desiredModGroupValues.OrderBy(c => c.Key)
                    .Select(c => $"{c.Key,-25} ({string.Join(",", c.Value.Select(e => e.Value.ToString("###,###")))})")
                    .ToList();

                var col2 = item.Explicits.OrderBy(c => c.Modifier.Group)
                    .Select(
                        c =>
                            $"{c.Modifier.Id,-25} ({string.Join(",", c.Values.Select(e => e.Value.ToString("###,###")))})")
                    .ToList();

                col1.Insert(0, "--------------------------------------");
                col2.Insert(0, "--------------------------------------");

                col1.Insert(0, "Desired result");
                col2.Insert(0, "Outcome");

                var p =
                    from idx in Enumerable.Range(0, Math.Max(col1.Count, col2.Count))
                    let c1 = col1.ElementAtOrDefault(idx) ?? string.Empty
                    let c2 = col2.ElementAtOrDefault(idx) ?? string.Empty
                    select $"{c1,-40} {c2}".PadRight(Console.WindowWidth - 5);

                Console.WriteLine(string.Join(Environment.NewLine, p));
            }

        }

    }
}