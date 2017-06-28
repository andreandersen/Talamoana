using Jil;

namespace Talamoana.Domain.Core.Stash
{
    public class AdditionalProperty
    {
        [JilDirective("name")]
        public string Name { get; set; }

        [JilDirective("values")]
        public object[][] Values { get; set; }

        [JilDirective("displayMode")]
        public int DisplayMode { get; set; }

        [JilDirective("progress")]
        public float Progress { get; set; }
    }
}
