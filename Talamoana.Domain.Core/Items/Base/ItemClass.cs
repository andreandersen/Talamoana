// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
using System;

namespace Talamoana.Domain.Core.Items.Base
{
    public class ItemClass : IItemClass, IEquatable<IItemClass>
    {
        public string Id { get; }
        public string Category { get; }
        public string Name { get; }

        public ItemClass(string id, string category, string name)
        {
            Id = id;
            Category = category;
            Name = name;
        }

        /// <inheritdoc />
        public bool Equals(IItemClass other) => string.Equals(Id, other?.Id);

        public bool Equals(ItemClass other) => string.Equals(Id, other?.Id);

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ItemClass) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => (Id != null ? Id.GetHashCode() : 0);

        public static bool operator ==(ItemClass a, IItemClass b) => a?.Equals(b) ?? false;

        public static bool operator !=(ItemClass a, IItemClass b) => !(a == b);

        public static bool operator ==(ItemClass a, ItemClass b) => a?.Equals(b) ?? false;

        public static bool operator !=(ItemClass a, ItemClass b) => !(a == b);
    }
}
