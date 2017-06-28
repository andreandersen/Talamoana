// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

using System;

namespace Talamoana.Domain.Core.Items.Base
{
    public class WeaponStats : IWeaponStats
    {
        public decimal AttacksPerSecond { get; }
        public decimal CritChance { get; }
        public int DamageMax { get; }
        public int DamageMin { get; }
        public decimal Dps => Math.Round(((DamageMax + DamageMin) / 2) * AttacksPerSecond, 2);
        public int Range { get; }

        public WeaponStats(decimal aps, decimal crit, int dmgMin, int dmgMax, int range)
        {
            AttacksPerSecond = Math.Round(aps, 2);
            CritChance = Math.Round(crit / 100m, 2);
            DamageMin = dmgMin;
            DamageMax = dmgMax;
            Range = range;
        }

       
    }
}
