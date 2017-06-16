using System.Collections.Generic;
using Talamoana.Domain.Core.Modifiers;
using Talamoana.Domain.Core.Shared;

namespace Talamoana.Domain.Core.Items.Crafting.Actions
{
    public class ExaltedOrb : IRandomCraftingAction
    {
        private readonly IRandomizer _randomizer;
        private readonly IReadOnlyList<IModifier> _allModifiers;

        /// <inheritdoc />
        public string Category => "Orb Crafting";

        /// <inheritdoc />
        public string Name => "Exalted Orb";

        public ExaltedOrb(IRandomizer randomizer, IReadOnlyList<IModifier> allModifiers)
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
        public void Apply(Item item) =>
            item.AddExplicitModifier(item.RandomizeNewModifier(_allModifiers.GetRollableExplicits(item)));

        /// <inheritdoc />
        public ForgingOutcome ForgeOutcome => ForgingOutcome.Addition;

        /// <inheritdoc />
        public RarityOutcome ExpectedRarity => RarityOutcome.Rare;

        /// <inheritdoc />
        public OriginalRarity OriginalRarity => OriginalRarity.Rare;
    }
}