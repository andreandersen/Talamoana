using Jil;
using System.Collections.Generic;

namespace Talamoana.Domain.Core.Stash
{
    public class StashResponse
    {
        [JilDirective("next_change_id")]
        public string NextChangeId { get; private set; }

        [JilDirective("stashes")]
        public List<Stash> Stashes { get; private set; }
    }
}
