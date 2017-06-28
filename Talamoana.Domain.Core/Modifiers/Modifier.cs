using System;
using System.Collections.Generic;
using System.Linq;

namespace Talamoana.Domain.Core.Modifiers
{
    /// <inheritdoc />
    public class Modifier : /*IModifier,*/ IEquatable<Modifier>
    {
        public Modifier(string id, string name, Domain domain, GenerationType generationType,
            List<ModifierStat> stats, List<string> addsTags, bool isEssenceOnly,
            string group, int requiredLevel, List<TagWeight> spawnTags,
            List<TagWeight> generationWeights)
        {
            Id = id;
            Name = name;
            Domain = domain;
            GenerationType = generationType;
            Stats = stats;
            AddsTags = addsTags;
            IsEssenceOnly = isEssenceOnly;
            Group = group;
            RequiredLevel = requiredLevel;
            SpawnWeights = spawnTags.ToArray();
            GenerationWeights = generationWeights;

            EphemeralGroupId = GetGroupIdentifier(group);
        }
        
        private static List<string> ObservedGroups = new List<string>();

        public static short GetGroupIdentifier(string groupId)
        {
            int idx = ObservedGroups.IndexOf(groupId);
            if (idx > 0) return (short)idx;
            ObservedGroups.Add(groupId);
            return (short)ObservedGroups.Count;
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
        public List<ModifierStat> Stats { get; }

        /// <inheritdoc />
        public List<string> AddsTags { get; }

        /// <inheritdoc />
        public bool IsEssenceOnly { get; }

        /// <inheritdoc />
        public string Group { get; }
        
        public short EphemeralGroupId { get; }

        /// <inheritdoc />
        public int RequiredLevel { get; }

        /// <inheritdoc />
        public TagWeight[] SpawnWeights { get; }

        /// <inheritdoc />
        public List<TagWeight> GenerationWeights { get; }


        #region Equality

        public bool Equals(Modifier other) => string.Equals(Id, other.Id);


        public override string ToString()
        {
            var spawnTags = string.Join(", ", SpawnWeights.Select(c => $"{c.TagId}:{c.Weight}"));
            return $"{Name} [{spawnTags}]";
        }

        public override bool Equals(object obj)
        {
            var m = obj as Modifier;
            return m != null && Equals(m);
        }

        public override int GetHashCode() => Id != null ? Id.GetHashCode() : 0;

        #endregion
    }
}