using System;
using Newtonsoft.Json;

namespace Talamoana.Domain.Core.Items.Weapons
{
    public class WeaponDamage
    {
        [JsonProperty]
        public decimal CritChance { get; private set; }

        [JsonProperty]
        public decimal AttacksPerSecond { get; private set; }

        [JsonProperty]
        public Damage PhysicalDamage { get; private set; }

        [JsonProperty]
        public Damage ChaosDamage { get; private set; }

        [JsonProperty]
        public Damage FireDamage { get; private set; }

        [JsonProperty]
        public Damage ColdDamage { get; private set; }

        [JsonProperty]
        public Damage LightningDamage { get; private set; }

        [JsonProperty]
        public decimal PhysicalDps =>
            Math.Round(PhysicalDamage.Avg * AttacksPerSecond, 2);

        [JsonProperty]
        public decimal ChaosDps =>
            Math.Round(ChaosDamage.Avg * AttacksPerSecond, 2);

        [JsonProperty]
        public decimal ElementalDps =>
            Math.Round((FireDamage.Avg +
            LightningDamage.Avg +
            ColdDamage.Avg) * AttacksPerSecond, 2);

        [JsonProperty]
        public decimal TotalDps =>
            PhysicalDps +
            ChaosDps +
            ElementalDps;
    }
}
