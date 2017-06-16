using System;
using System.Collections.Generic;
using System.Linq;
using Talamoana.Domain.Core.Items;
using Talamoana.Domain.Core.Modifiers.Exceptions;
using Talamoana.Domain.Core.Shared;
using Talamoana.Domain.Core.Stats;

namespace Talamoana.Domain.Core.Modifiers
{
    public static class ModifierExtensions
    {
        private static readonly IRandomizer Randomizer = new PseudoRandom();

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
        public static MaterializedModifier Materialize(this IModifier modifier, Dictionary<IStat, int> values) =>
            new MaterializedModifier(modifier, values);

        public static int GetSpawnWeight(this IModifier modifier, Item item) =>
            GetSpawnWeightImpl(item, modifier);

        public static int GetSpawnWeight(this Item item, IModifier modifier) =>
            GetSpawnWeightImpl(item, modifier);

        private static int GetSpawnWeightImpl(this Item item, IModifier modifier)
        {
            TagWeight selectedTag = null;
            for (var index = 0; index < modifier.SpawnWeights.Count; index++)
            {
                var c = modifier.SpawnWeights[index];
                var any = false;
                for (var qq = 0; qq < item.Tags.Count; qq++)
                {
                    var q = item.Tags[qq];
                    if (c.TagId == q)
                    {
                        any = true;
                        break;
                    }
                }
                if (any)
                {
                    selectedTag = c;
                    break;
                }
            }

            return selectedTag?.Weight ?? 0;

            //var factor = modifier.GenerationWeights
            //    .FirstOrDefault(p => item.Tags.Contains(p.TagId))?.Weight / 100d ?? 1;

            //return Convert.ToInt32((selectedTag?.Weight ?? 0) * factor);
        }

        public static int Count(this IEnumerable<IMaterializedModifier> modifiers, GenerationType genType) =>
            modifiers.Count(c => c.Modifier.GenerationType == genType);

        /// <summary>
        ///     This is used to find rollable mods which is used to roll crafting orbs. It will
        ///     assume that the modifiers required are either prefix/suffix and item domain, with a
        ///     spawn weight that is higher than 0.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="allModifiers"></param>
        /// <returns></returns>
        public static IReadOnlyList<IModifier> GetRollableExplicits(this IEnumerable<IModifier> allModifiers, Item item)
        {
            var maxAffixCount = item.Rarity == ItemRarity.Rare ? 3 : (item.Rarity == ItemRarity.Magic ? 1 : 0);

            var prefixCount = item.Explicits.Count(GenerationType.Prefix);
            var suffixCount = item.Explicits.Count(GenerationType.Suffix);

            var canHaveMorePrefixes = prefixCount < maxAffixCount;
            var canHaveMoreSuffixes = suffixCount < maxAffixCount;

            var existingModifierGroups = item.Explicits.Select(c => c.Modifier.Group).ToList();

            var result = allModifiers
                .Where(mod => mod.Domain == Domain.Item &&
                              (mod.GenerationType == GenerationType.Prefix ||
                               mod.GenerationType == GenerationType.Suffix) &&
                              (mod.GenerationType != GenerationType.Prefix || canHaveMorePrefixes) &&
                              (mod.GenerationType != GenerationType.Suffix || canHaveMoreSuffixes) &&
                              !existingModifierGroups.Contains(mod.Group) && mod.RequiredLevel <= item.ItemLevel &&
                              mod.GetSpawnWeight(item) > 0).ToList();

            return result;
        }

        public static IMaterializedModifier RandomizeNewModifier(this Item item,
            IReadOnlyList<IModifier> modifiersToUse)
        {
            var totalWeight = 0;
            for (var index = 0; index < modifiersToUse.Count; index++)
            {
                var p = modifiersToUse[index];
                totalWeight += GetSpawnWeight(p, item);
            }
            
            var rand = Randomizer.Next(1, totalWeight);
            
            int sum = 0;
            for (int j = 0; j < modifiersToUse.Count; j++)
            {
                sum += modifiersToUse[j].GetSpawnWeight(item);
                if (sum >= rand)
                {
                    var values = modifiersToUse[j].Stats.ToDictionary(p => p.Stat, p => Randomizer.Next(p.Min, p.Max));
                    return modifiersToUse[j].Materialize(values);
                }
            }

            throw new IndexOutOfRangeException();
        }
    }
}

//P attack_maximum_added_physical_damage 13-15
//S base_resist_all_elements_% 15-16
//P base_maximum_life 70-79
//S attack_speed_+% 5-7
//S base_lightning_damage_resistance_% 40
//P base_item_found_rarity_+% 25