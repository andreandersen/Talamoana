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

        /// <inheritdoc />
        public void Apply(Item item)
        {
            // 4, 5, 6 mods respectively
            var chaosChances = new[] { 650, 275, 75 };
            var rand = _randomizer.Next(1, chaosChances.Sum());
            var mods = rand < chaosChances[0] ? 4 : (rand < chaosChances[0] + chaosChances[1] ? 5 : 6);

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