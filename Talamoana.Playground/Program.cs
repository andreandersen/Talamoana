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
        private static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Run();

            if (Debugger.IsAttached)
            {
                Console.WriteLine("\r\n\r\nPress any key to exit...");
                Console.ReadKey(true);
            }
        }

        private static void Run()
        {
            var allMods = new JsonModsReader(new JsonStatsReader()).Read()
                .Where(p => p.Domain == Domain.Core.Modifiers.Domain.Item &&
                            (p.GenerationType == GenerationType.Prefix ||
                             p.GenerationType == GenerationType.Suffix)).ToList();

            var baseItems = new JsonItemReader(allMods).Read().ToList();
            var baseItem = baseItems.First(p => p.Name == "Steel Ring");

            var item = new Item(baseItem, 84) { Rarity = ItemRarity.Magic };

            allMods = allMods.GetRollableExplicits(item).ToList();

            //var mods = allMods.GetRollableExplicits(item)
            //    .Select(c => new
            //    {
            //        Affix = c.GenerationType.ToString(),
            //        c.Group,
            //        c.Id,
            //        Stats = string.Join(", ", c.Stats.Select(e => $"{e.Stat.Id} [ {e.Min} - {e.Max} ]"))
            //    }).ToList();

            var lifeMod = allMods.First(p => p.Id == "IncreasedLife7"); // Prefix 70 - 79
            var physMod = allMods.First(p => p.Id == "AddedPhysicalDamage6"); // Prefix 9 - 15
            var evaMod = allMods.First(p => p.Id == "IncreasedEvasionRating7"); // Prefix 151 - 170

            var allResMod = allMods.First(p => p.Id == "AllResistances5"); // Suffix 13 - 16
            var atkSpdMod = allMods.First(p => p.Id == "IncreasedAttackSpeed1"); // Suffix 5 - 7
            var chaosResMod = allMods.First(p => p.Id == "ChaosResist6"); // Suffix 31 - 35

            var desiredModifiers = new List<IMaterializedModifier>
            {
                lifeMod.Materialize(new Dictionary<IStat, int> { { lifeMod.Stats[0].Stat, 70 } }),
                //physMod.Materialize(new Dictionary<IStat, int> { { physMod.Stats[0].Stat, 6 }, { physMod.Stats[1].Stat, 11 } }),
                //evaMod.Materialize(new Dictionary<IStat, int> { { evaMod.Stats[0].Stat, 100 } }),
                allResMod.Materialize(new Dictionary<IStat, int> { { allResMod.Stats[0].Stat, 10 } }),
                //atkSpdMod.Materialize(new Dictionary<IStat, int> { { atkSpdMod.Stats[0].Stat, 5 } }),
                //chaosResMod.Materialize(new Dictionary<IStat, int> { { chaosResMod.Stats[0].Stat, 25 } })
            }.ToDictionary(
                p => p.Modifier.Group,
                p => (IReadOnlyDictionary<IStat, int>)p.Values.ToDictionary(c => c.Key, c => c.Value));


            //var strategy = new AltCraftingStrategy(allMods);

            var queue = new ConcurrentQueue<int>(Enumerable.Range(0, 1000));
            var costResults = new ConcurrentBag<IReadOnlyDictionary<Type, int>>();
            int concurrency = 4;

            var tasks = new Task[concurrency];

            for (int i = 0; i < concurrency; i++)
            {
                var t = Task.Run(() =>
                {
                    while (queue.TryDequeue(out var _))
                    {
                        var result = new AltCraftingStrategy(allMods, new CryptoRandom()).Execute(
                            new Item(baseItem, 84) { Rarity = ItemRarity.Magic },
                            desiredModifiers, CancellationToken.None);

                        costResults.Add(result);
                    }
                });

                tasks[i] = t;
            }
            
            new Thread(() =>
            {
                while (true)
                {
                    PrintUpdate(queue, costResults, tasks);
                    Thread.Sleep(1000);
                }
            }).Start();

            Task.WaitAll(tasks);
        }

        private static void PrintUpdate(ConcurrentQueue<int> queue, ConcurrentBag<IReadOnlyDictionary<Type, int>> costResults, Task[] tasks)
        {
            lock (Talamoana.Util.ConsoleLock)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Queue: {queue.Count,-5} Concurrency: {tasks.Length.ToString().PadRight(10)}");
                Console.WriteLine("\r\n");
                var result = costResults.SelectMany(c => c).GroupBy(c => c.Key, c => c.Value)
                    .ToDictionary(c => c.Key, c => c.Average());
                result.ToList().ForEach(c => { Console.WriteLine($"{c.Key.Name,-20} {c.Value,10:###,###}"); });

                Console.WriteLine(DateTime.Now);
            }
        }
    }
}