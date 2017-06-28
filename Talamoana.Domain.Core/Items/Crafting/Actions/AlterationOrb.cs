using System.Collections.Generic;
using Talamoana.Domain.Core.Modifiers;
using Talamoana.Domain.Core.Shared;

namespace Talamoana.Domain.Core.Items.Crafting.Actions
{
    public class AlterationOrb : IRandomCraftingAction
    {
        private readonly IRandomizer _randomizer;
        private readonly List<Modifier> _allModifiers;

        /// <inheritdoc />
        public string Category => "Orb Crafting";

        /// <inheritdoc />
        public string Name => "Alteration Orb";

        public AlterationOrb(IRandomizer randomizer, List<Modifier> allModifiers)
        {
            _randomizer = randomizer;
            _allModifiers = allModifiers;
        }

        /// <inheritdoc />
        public bool IsApplicable(Item item) => 
            item.Rarity == ItemRarity.Magic && 
            !item.IsCorrupted &&
            _allModifiers.GetRollableExplicits(item).Count > 0;

        /// <inheritdoc />
        public void Apply(Item item)
        {
            item.ClearExplicitModifiers();
            var numberOfMods = _randomizer.Next(1, 2);

            for (int i = 0; i < numberOfMods; i++)
            {
                var mod = item.RandomizeNewModifier(_allModifiers.GetRollableExplicits(item));
                item.AddExplicitModifier(mod);
            }
        }

        /// <inheritdoc />
        public ForgingOutcome ForgeOutcome => ForgingOutcome.Reforge;

        /// <inheritdoc />
        public RarityOutcome ExpectedRarity => RarityOutcome.Magic;

        /// <inheritdoc />
        public OriginalRarity OriginalRarity => OriginalRarity.Magic;
    }
}