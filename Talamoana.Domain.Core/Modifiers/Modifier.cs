using System;
using System.Collections.Generic;
using System.Linq;

namespace Talamoana.Domain.Core.Modifiers
{
    /// <inheritdoc />
    public class Modifier : IModifier, IEquatable<Modifier>
    {
        public Modifier(string id, string name, Domain domain, GenerationType generationType,
            IList<ModifierStat> stats, IList<string> addsTags, bool isEssenceOnly,
            string group, int requiredLevel, IList<TagWeight> spawnTags,
            IList<TagWeight> generationWeights)
        {
            Id = id;
            Name = name;
            Domain = domain;
            GenerationType = generationType;
            Stats = (IReadOnlyList<ModifierStat>) stats;
            AddsTags = (IReadOnlyList<string>) addsTags;
            IsEssenceOnly = isEssenceOnly;
            Group = group;
            RequiredLevel = requiredLevel;
            SpawnWeights = (IReadOnlyList<TagWeight>) spawnTags;
            GenerationWeights = (IReadOnlyList<TagWeight>) generationWeights;
        }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public Domain Domain { get; }

        /// <inheritdoc />
        public GenerationType GenerationType { get; }

        /// <inheritdoc />
        public IReadOnlyList<ModifierStat> Stats { get; }

        /// <inheritdoc />
        public IReadOnlyList<string> AddsTags { get; }

        /// <inheritdoc />
        public bool IsEssenceOnly { get; }

        /// <inheritdoc />
        public string Group { get; }

        /// <inheritdoc />
        public int RequiredLevel { get; }

        /// <inheritdoc />
        public IReadOnlyList<TagWeight> SpawnWeights { get; }

        /// <inheritdoc />
        public IReadOnlyList<TagWeight> GenerationWeights { get; }


        #region Equality

        public bool Equals(Modifier other) => string.Equals(Id, other.Id);

        public bool Equals(IModifier other) => string.Equals(Id, other.Id);

        public override string ToString()
        {
            var spawnTags = string.Join(", ", SpawnWeights.Select(c => $"{c.TagId}:{c.Weight}"));
            return $"{Name} [{spawnTags}]";
        }

        public override bool Equals(object obj)
        {
            var m = obj as Modifier;
            return m == null || Equals(m);
        }

        public override int GetHashCode() => Id != null ? Id.GetHashCode() : 0;

        #endregion
    }
}