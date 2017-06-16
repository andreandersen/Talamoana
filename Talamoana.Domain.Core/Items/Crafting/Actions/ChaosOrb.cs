using System.Collections.Generic;
using System.Linq;
using Talamoana.Domain.Core.Modifiers;
using Talamoana.Domain.Core.Shared;

namespace Talamoana.Domain.Core.Items.Crafting.Actions
{
    public class ChaosOrb : IRandomCraftingAction
    {
        private readonly IRandomizer _randomizer;
        private readonly IReadOnlyList<IModifier> _allModifiers;

        /// <inheritdoc />
        public string Category => "Orb Crafting";

        /// <inheritdoc />
        public string Name => "Chaos Orb";

        public ChaosOrb(IRandomizer randomizer, IReadOnlyList<IModifier> allModifiers)
        {
            _randomizer = randomizer;
            _allModifiers = allModifiers;
        }

        /// <inheritdoc />
        public bool IsApplicable(Item item) =>
            item.Rarity == ItemRarity.Rare &&
            !item.IsCorrupted &&
            _allModifiers.GetRollableExplicits(item).Count > 0;

        // 4, 5, 6 mods respectively
        private static readonly int[] ChaosChances = new[] { 650, 275, 75 };

        /// <inheritdoc />
        public void Apply(Item item)
        {
            var rand = _randomizer.Next(1, 1000);
            
            var mods = 4;
            
            if (rand > ChaosChances[0] + ChaosChances[1])
                mods = 6;
            else if (rand > ChaosChances[0])
                mods = 5;

            item.Reset();
            item.Rarity = ItemRarity.Rare;
            
            for (int i = 0; i < mods; i++)
            {
                var mod = item.RandomizeNewModifier(_allModifiers.GetRollableExplicits(item));
                item.AddExplicitModifier(mod);
            }
        }

        /// <inheritdoc />
        public ForgingOutcome ForgeOutcome => ForgingOutcome.Reforge;

        /// <inheritdoc />
        public RarityOutcome ExpectedRarity => RarityOutcome.Rare;

        /// <inheritdoc />
        public OriginalRarity OriginalRarity => OriginalRarity.Rare;
    }
}