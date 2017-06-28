//using System;
//using System.Collections.Generic;
//using Talamoana.Domain.Core.Items;

//namespace Talamoana.Domain.Core.Modifiers
//{
//    /// <summary>
//    ///     Represents an abstract modifier from Mods.dat
//    /// </summary>
//    public interface IModifier : IEquatable<Modifier>
//    {
//        /// <summary>
//        ///     Internal Id of the modifier
//        /// </summary>
//        string Id { get; }

//        /// <summary>
//        ///     Name of the mod, used to generate the "name" of a magic item
//        /// </summary>
//        string Name { get; }

//        /// <inheritdoc />
//        Domain Domain { get; }

//        /// <inheritdoc />
//        GenerationType GenerationType { get; }

//        /// <summary>
//        ///     Stats that are applied with this mod.
//        /// </summary>
//        List<ModifierStat> Stats { get; }

//        /// <summary>
//        ///     When applying this mod to an item, these tags will be added in consideration for conesecutive mods added.
//        /// </summary>
//        List<string> AddsTags { get; }

//        /// <summary>
//        ///     If modifier is only obtainable through Essence roll
//        /// </summary>
//        bool IsEssenceOnly { get; }

//        /// <summary>
//        ///     Modifier group identifier
//        /// </summary>
//        string Group { get; }

//        /// <summary>
//        ///     Required item level for the modifier to spawn
//        /// </summary>
//        int RequiredLevel { get; }

//        /// <summary>
//        ///     The weight, used to calculate the probability of a modifier spawning on its subject.
//        /// </summary>
//        List<TagWeight> SpawnWeights { get; }

//        /// <summary>
//        ///     Weight factor when tag is present from previously added modifiers.
//        /// </summary>
//        List<TagWeight> GenerationWeights { get; }
//    }
//}