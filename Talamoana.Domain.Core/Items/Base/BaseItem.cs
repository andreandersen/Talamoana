using System.Collections.Generic;
using System.Linq;
using Talamoana.Domain.Core.Modifiers;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
namespace Talamoana.Domain.Core.Items.Base
{
    public class BaseItem : IBaseItem
    {
        public string Id { get; }
        public string Name { get; }
        public IItemClass ItemClass { get; }
        public int DropLevel { get;  }
        public int Height { get; }

        /// <inheritdoc />
        public IShieldStats ShieldStats { get; }
        public int Width { get; }
        public IArmourStats ArmourStats { get; }
        public IWeaponStats WeaponStats { get; }
        public IAttributeRequirements AttributeRequirements { get; }
        public IReadOnlyList<string> Tags { get; }
        public IReadOnlyList<IModifier> Implicits { get; }

        public BaseItem(string id, string name, IItemClass itemClass, int dropLevel, int height, int width, IArmourStats armourStats,
            IWeaponStats weaponStats, IShieldStats shieldStats, IAttributeRequirements attrRequirements, IEnumerable<string> tags, IEnumerable<IModifier> implicits)
        {
            Id = id;
            Name = name;
            ItemClass = itemClass;
            DropLevel = dropLevel;
            Height = height;
            Width = width;
            ArmourStats = armourStats;
            WeaponStats = weaponStats;
            ShieldStats = shieldStats;
            AttributeRequirements = attrRequirements;
            Tags = tags.ToList().AsReadOnly();
            Implicits = implicits.ToList().AsReadOnly();
        }
    }
}
