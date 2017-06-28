using System;
using System.Collections.Generic;

namespace Talamoana.Domain.Core.Modifiers
{
    /// <summary>
    ///     Used to represent spawn weight for calculating spawn probability
    /// </summary>
    public class TagWeight
    {
        private static readonly List<string> ObservedTags = new List<string>();

        public static short GetTagIdentifier(string stringId)
        {
            int idx = ObservedTags.IndexOf(stringId);
            if (idx > 0) return (short)idx;
            ObservedTags.Add(stringId);
            return (short)ObservedTags.Count;
        }
        
        public TagWeight(string tag, int weight)
        {
            EphemeralIdentifier = GetTagIdentifier(tag);
            TagId = tag;
            Weight = weight;
        }

        public short EphemeralIdentifier { get; }
        public string TagId { get; }
        public int Weight { get; }
    }
}