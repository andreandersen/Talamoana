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
        List<Modifier> Implicits { get; }
        IItemClass ItemClass { get; }
        string Name { get; }
        List<string> Tags { get; }
        IWeaponStats WeaponStats { get; }
        IShieldStats ShieldStats { get; }
        int Width { get; }
        string ImageUrl { get; }
    }
}