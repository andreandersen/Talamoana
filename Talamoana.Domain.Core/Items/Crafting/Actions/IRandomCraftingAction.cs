namespace Talamoana.Domain.Core.Items.Crafting.Actions
{
    public interface IRandomCraftingAction : ICraftingAction
    {
        string Name { get; }
        bool IsApplicable(Item item);
        void Apply(Item item);

        ForgingOutcome ForgeOutcome { get; }
        RarityOutcome ExpectedRarity { get; }
        OriginalRarity OriginalRarity { get; }
    }
}