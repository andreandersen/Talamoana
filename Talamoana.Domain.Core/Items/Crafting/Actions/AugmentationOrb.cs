using System.Collections.Generic;
using Talamoana.Domain.Core.Modifiers;

namespace Talamoana.Domain.Core.Items.Crafting.Actions
{
    public class AugmentationOrb : IRandomCraftingAction
    {
        private readonly List<Modifier> _allModifiers;

        /// <inheritdoc />
        public string Category => "Orb Crafting";

        /// <inheritdoc />
        public string Name => "Augmentation Orb";

        public AugmentationOrb(List<Modifier> allModifiers)
        {
            _allModifiers = allModifiers;
        }

        /// <inheritdoc />
        public bool IsApplicable(Item item) =>
            item.Rarity == ItemRarity.Magic &&
            !item.IsCorrupted &&
            _allModifiers.GetRollableExplicits(item).Count > 0;

        /// <inheritdoc />
        public void Apply(Item item) =>
            item.AddExplicitModifier(item.RandomizeNewModifier(_allModifiers.GetRollableExplicits(item)));

        /// <inheritdoc />
        public ForgingOutcome ForgeOutcome => ForgingOutcome.Addition;

        /// <inheritdoc />
        public RarityOutcome ExpectedRarity => RarityOutcome.Magic;

        /// <inheritdoc />
        public OriginalRarity OriginalRarity => OriginalRarity.Magic;
    }
}