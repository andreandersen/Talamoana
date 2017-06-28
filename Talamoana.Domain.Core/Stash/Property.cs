using Jil;
using System.Collections.Generic;

namespace Talamoana.Domain.Core.Stash
{
    public class Property
    {
        [JilDirective("name")]
        public string Name { get; set; }

        [JilDirective("values")]
        public List<List<object>> Values { get; set; }

        [JilDirective("displayMode")]
        public int DisplayMode { get; set; }

        [JilDirective("type")]
        public int Type { get; set; }
    }
}
