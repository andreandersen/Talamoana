// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace Talamoana.Domain.Core.Items.Base
{
    public class ArmourStats : IArmourStats
    {
        public int Armour { get; }
        public int EnergyShield { get; }
        public int Evasion { get; }

        public ArmourStats(int arm, int es, int eva)
        {
            Armour = arm;
            EnergyShield = es;
            Evasion = eva;
        }
    }
}
