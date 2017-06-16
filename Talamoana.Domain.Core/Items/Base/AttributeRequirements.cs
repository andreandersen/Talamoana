// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace Talamoana.Domain.Core.Items.Base
{
    public class AttributeRequirements : IAttributeRequirements
    {
        public int Dexterity { get; }
        public int Intelligence { get; }
        public int Strength { get; }

        public AttributeRequirements(int dex, int @int, int str)
        {
            Dexterity = dex;
            Intelligence = @int;
            Strength = str;
        }
    }
}
