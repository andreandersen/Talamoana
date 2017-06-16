using System;

namespace Talamoana.Domain.Core.Items.Crafting.Actions
{
    [Flags]
    public enum OriginalRarity
    {
        Normal = 0,
        Magic  = 1,
        Rare   = 2,
        Unique = 4
    }
}