using System.Collections.Generic;
using Talamoana.Domain.Core.Modifiers;

namespace Talamoana.Domain.Core.Items.Base
{
    public interface IBaseItem
    {
        IArmourStats ArmourStats { get; }
        IAttributeRequirements AttributeRequirements { get; }
        int DropLevel { get; }
        int Height { get; }
        string Id { get; }
        IReadOnlyList<IModifier> Implicits { get; }
        IItemClass ItemClass { get; }
        string Name { get; }
        IReadOnlyList<string> Tags { get; }
        IWeaponStats WeaponStats { get; }
        IShieldStats ShieldStats { get; }
        int Width { get; }
    }
}