using System;
using Newtonsoft.Json;

namespace Talamoana.Domain.Core.Items.Weapons
{
    public class Damage
    {
        [JsonProperty]
        public int Min { get; set; }

        [JsonProperty]
        public int Max { get; set; }

        [JsonProperty]
        public decimal Avg => Math.Round((Min + Max) / 2m, 2);

        public Damage(int min, int max)
        {
            Min = min;
            Max = max;
        }

        [JsonConstructor]
        private Damage() { }
    }
}
