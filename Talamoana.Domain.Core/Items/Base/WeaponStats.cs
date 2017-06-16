// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace Talamoana.Domain.Core.Items.Base
{
    public class WeaponStats : IWeaponStats
    {
        public decimal AttacksPerSecond { get; }
        public decimal CritChance { get; }
        public int DamageMax { get; }
        public int DamageMin { get; }
        public int Range { get; }

        public WeaponStats(decimal aps, decimal crit, int dmgMin, int dmgMax, int range)
        {
            AttacksPerSecond = aps;
            CritChance = crit;
            DamageMin = dmgMin;
            DamageMax = dmgMax;
            Range = range;
        }
    }
}
