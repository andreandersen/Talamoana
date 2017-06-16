namespace Talamoana.Domain.Core.Items.Crafting.Actions
{
    public class ScourOrb : IRandomCraftingAction
    {
        /// <inheritdoc />
        public string Category => "Orb Crafting";

        /// <inheritdoc />
        public string Name => "Scour Orb";

        /// <inheritdoc />
        public bool IsApplicable(Item item) =>
            !item.IsCorrupted && item.Rarity != ItemRarity.Normal && item.Rarity != ItemRarity.Unique;

        /// <inheritdoc />
        public void Apply(Item item) => item.Reset();

        /// <inheritdoc />
        public ForgingOutcome ForgeOutcome => ForgingOutcome.Clear;

        /// <inheritdoc />
        public RarityOutcome ExpectedRarity => RarityOutcome.Normal;

        /// <inheritdoc />
        public OriginalRarity OriginalRarity => OriginalRarity.Magic | OriginalRarity.Rare;
    }
}