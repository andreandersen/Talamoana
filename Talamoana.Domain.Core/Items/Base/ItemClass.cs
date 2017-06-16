// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace Talamoana.Domain.Core.Items.Base
{
    public class ItemClass : IItemClass
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
    }
}
