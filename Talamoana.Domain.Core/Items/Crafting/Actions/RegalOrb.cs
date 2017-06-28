using System.Collections.Generic;
using Talamoana.Domain.Core.Modifiers;
using Talamoana.Domain.Core.Shared;

namespace Talamoana.Domain.Core.Items.Crafting.Actions
{
    public class RegalOrb : IRandomCraftingAction
    {
        private readonly IRandomizer _randomizer;
        private readonly List<Modifier> _allModifiers;

        /// <inheritdoc />
        public string Category => "Orb Crafting";

        /// <inheritdoc />
        public string Name => "Regal Orb";

        public RegalOrb(IRandomizer randomizer, List<Modifier> allModifiers)
        {
            _randomizer = randomizer;
            _allModifiers = allModifiers;
        }

        /// <inheritdoc />
        public bool IsApplicable(Item item) =>
            item.Rarity == ItemRarity.Magic &&
            !item.IsCorrupted;

        /// <inheritdoc />
        public void Apply(Item item)
        {
            item.Rarity = ItemRarity.Rare;
            var mod = item.RandomizeNewModifier(_allModifiers.GetRollableExplicits(item));
            item.AddExplicitModifier(mod);
        }

        /// <inheritdoc />
        public ForgingOutcome ForgeOutcome => ForgingOutcome.Addition;

        /// <inheritdoc />
        public RarityOutcome ExpectedRarity => RarityOutcome.Magic;

        /// <inheritdoc />
        public OriginalRarity OriginalRarity => OriginalRarity.Rare;
    }
}