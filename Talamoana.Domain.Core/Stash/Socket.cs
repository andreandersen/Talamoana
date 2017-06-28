using Jil;
using Newtonsoft.Json;

namespace Talamoana.Domain.Core.Stash
{
    public class Socket
    {
        [JilDirective("group")]
        public int Group { get; set; }

        [JilDirective("attr")]
        public string Attribute { get; set; }
    }
}
