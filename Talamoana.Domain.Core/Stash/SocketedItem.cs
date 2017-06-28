using Jil;

namespace Talamoana.Domain.Core.Stash
{
    public class SocketedItem
    {
        [JilDirective("ilvl")]
        public int ItemLevel { get; set; }

        [JilDirective("icon")]
        public string IconUrl { get; set; }

        [JilDirective("support")]
        public bool IsSupport { get; set; }

        [JilDirective("id")]
        public string Id { get; set; }

        [JilDirective("name")]
        public string Name { get; set; }

        [JilDirective("typeLine")]
        public string TypeLine { get; set; }

        [JilDirective("corrupted")]
        public bool IsCorrupted { get; set; }

        [JilDirective("locketToCharacter")]
        public bool IsLockedToCharacter { get; set; }

        [JilDirective("properties")]
        public Property[] Properties { get; set; }

        [JilDirective("additionalProperties")]
        public AdditionalProperty[] AdditionalProperties { get; set; }

        [JilDirective("requirements")]
        public Requirement[] Requirements { get; set; }

        [JilDirective("explicitMods")]
        public string[] ExplicitMods { get; set; }

        [JilDirective("socket")]
        public int Socket { get; set; }

        [JilDirective("colour")]
        public string Colour { get; set; }
    }
}
