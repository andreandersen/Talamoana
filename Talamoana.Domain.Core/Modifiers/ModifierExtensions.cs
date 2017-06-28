using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Talamoana.Domain.Core.Items;
using Talamoana.Domain.Core.Shared;
using Talamoana.Domain.Core.Stats;

namespace Talamoana.Domain.Core.Modifiers
{
    public static class ModifierExtensions
    {
        private static readonly IRandomizer Randomizer = new CryptoRandom();

        /// <summary>
        ///     Materializes a Modifier with values, which is used on materialized items.
        /// </summary>
        /// <param name="modifier"><see cref="Modifier">Modifier</see> to materialize</param>
        /// <param name="values">Values that are being set. Must meet the stats used in the modifier</param>
        /// <returns>
        ///     <see cref="MaterializedModifier">Materialized modifier</see>
        /// </returns>
        /// <exception cref="ValuesDoNotMatchModifierException">
        ///     Thrown if <paramref name="values" /> don't match
        ///     <paramref name="modifier" /> stats
        /// </exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="modifier" /> or <paramref name="values" /> is null</exception>
        public static MaterializedModifier Materialize(this Modifier modifier, Dictionary<Stat, int> values) =>
            new MaterializedModifier(modifier, values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetSpawnWeight(this Modifier modifier, Item item) =>
            GetSpawnWeightImpl(item, modifier);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetSpawnWeight(this Item item, Modifier modifier) =>
            GetSpawnWeightImpl(item, modifier);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetSpawnWeightImpl(this Item item, Modifier modifier)
        {
            for (int i = 0; i < modifier.SpawnWeights.Length; i++)
            {
                var modSpawn = modifier.SpawnWeights[i];
                for (int j = item.Tags.Count - 1; j > -1; j--)
                    if (item.Tags[j] == modSpawn.EphemeralIdentifier)
                        return modSpawn.Weight;
            }
            return 0;
        }

        //var factor = modifier.GenerationWeights
        //    .FirstOrDefault(p => item.Tags.Contains(p.TagId))?.Weight / 100d ?? 1;

        //return Convert.ToInt32((selectedTag?.Weight ?? 0) * factor);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Count(this List<MaterializedModifier> modifiers, GenerationType genType)
        {
            int count = 0;
            for (var index = modifiers.Count - 1; index >= 0; index--)
            {
                var c = modifiers[index];
                if (c.Modifier.GenerationType == genType) count++;
            }
            return count;
        }

        /// <summary>
        ///     This is used to find rollable mods which is used to roll crafting orbs. It will
        ///     assume that the modifiers required are either prefix/suffix and item domain, with a
        ///     spawn weight that is higher than 0.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="allModifiers"></param>
        /// <returns></returns>
        public static List<Modifier> GetRollableExplicits(this List<Modifier> allModifiers, Item item)
        {
            var maxAffixCount = item.Rarity == ItemRarity.Rare ? 3 : (item.Rarity == ItemRarity.Magic ? 1 : 0);

            var canHaveMorePrefixes = item.Explicits.Count(GenerationType.Prefix) < maxAffixCount;
            var canHaveMoreSuffixes = item.Explicits.Count(GenerationType.Suffix) < maxAffixCount;

            var existingModifierGroups = item.Explicits.Select(c => c.Modifier.EphemeralGroupId).ToList();

            var result = allModifiers
                .Where(mod => mod.Domain == Domain.Item &&
                              ((mod.GenerationType == GenerationType.Prefix && canHaveMorePrefixes) ||
                              (mod.GenerationType == GenerationType.Suffix && canHaveMoreSuffixes)) &&
                              !existingModifierGroups.Contains(mod.EphemeralGroupId) &&
                              mod.RequiredLevel <= item.ItemLevel
                              && mod.GetSpawnWeight(item) > 0);

            return result.ToList();
        }

        public static MaterializedModifier RandomizeNewModifier(this Item item,
            List<Modifier> modifiersToUse)
        {
            var totalWeight = 0;
            for (var index = modifiersToUse.Count - 1; index >= 0; index--)
            {
                var p = modifiersToUse[index];
                totalWeight += GetSpawnWeight(p, item);
            }

            var rand = Randomizer.Next(1, totalWeight);

            int sum = 0;
            for (int j = modifiersToUse.Count - 1; j >= 0; j--)
            {
                sum += modifiersToUse[j].GetSpawnWeight(item);
                if (sum < rand) continue;
                var values = modifiersToUse[j].Stats.ToDictionary(p => p.Stat, p => Randomizer.Next(p.Min, p.Max));
                return modifiersToUse[j].Materialize(values);
            }

            throw new IndexOutOfRangeException();
        }
    }
}