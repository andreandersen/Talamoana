// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace Talamoana.Domain.Core.Items.Base
{
    public class ShieldStats : IShieldStats
    {
        public int Block { get; }

        public ShieldStats(int block)
        {
            Block = block;
        }
    }
}
