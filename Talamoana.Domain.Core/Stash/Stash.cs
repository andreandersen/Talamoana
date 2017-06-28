using Jil;
using System.Collections.Generic;

namespace Talamoana.Domain.Core.Stash
{
    public class Stash
    {
        [JilDirective("accountName")]
        public string AccountName { get; set; }

        [JilDirective("lastCharacterName")]
        public string LastCharacterName { get; set; }

        [JilDirective("id")]
        public string Id { get; set; }

        [JilDirective("stash")]
        public string StashName { get; set; }

        [JilDirective("stashType")]
        public string StashType { get; set; }

        [JilDirective("public")]
        public bool IsPublic { get; set; }

        [JilDirective("items")]
        public List<StashItem> Items { get; set; }
    }
}
