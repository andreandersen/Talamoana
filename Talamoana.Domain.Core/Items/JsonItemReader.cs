using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Talamoana.Domain.Core.Items.Base;
using Talamoana.Domain.Core.Modifiers;

namespace Talamoana.Domain.Core.Items
{
    public class JsonItemReader
    {
        private readonly string _file;
        private readonly Dictionary<string, Modifier> _modDict;

        public JsonItemReader(List<Modifier> mods, string sourceFile = "Data\\items.json")
        {
            _modDict = mods.ToDictionary(p => p.Id, p => p);
            _file = sourceFile;
        }

        public IEnumerable<BaseItem> Read()
        {
            var json = File.ReadAllText(_file);
            var objects = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(json);

            foreach (var item in objects)
            {
                var o = item.Value;
                var ic = o["class"].ToObject<JObject>();
                var ars = o["armour_stats"].ToObject<JObject>();
                var ws = o["weapon_stats"].ToObject<JObject>();
                var atr = o["attribute_requirements"].ToObject<JObject>();
                var shield = o["shield_stats"].ToObject<JObject>();

                var implicits = ((JArray) o["implicits"])
                    .Select(c => c.Value<string>())
                    .Where(c => _modDict.ContainsKey(c))
                    .Select(c => _modDict[c])
                    .ToList();

                yield return new BaseItem
                (
                    o["id"].ToString(),
                    o["name"].ToString(),
                    new ItemClass(ic["id"].ToString(), ic["category"].ToString(), ic["name"].ToString()),
                    o.Value<int>("drop_level"),
                    o.Value<int>("height"),
                    o.Value<int>("width"),
                    ars != null
                        ? new ArmourStats(ars.Value<int>("armour"), ars.Value<int>("energy_shield"),
                            ars.Value<int>("evasion"))
                        : null,
                    ws != null
                        ? new WeaponStats(ws.Value<decimal>("aps"), ws.Value<decimal>("crit"), ws.Value<int>("dmg_min"),
                            ws.Value<int>("dmg_max"), ws.Value<int>("range"))
                        : null,
                    shield != null
                        ? new ShieldStats(shield.Value<int>("block"))
                        : null,
                    atr != null
                        ? new AttributeRequirements(atr.Value<int>("dex"), atr.Value<int>("int"), atr.Value<int>("str"))
                        : null,
                    ((JArray) o["tags"]).Select(p => p.ToString()).ToList(),
                    implicits,
                    o["visual_identity"].Value<string>("dds")
                );
            }
        }
    }
}