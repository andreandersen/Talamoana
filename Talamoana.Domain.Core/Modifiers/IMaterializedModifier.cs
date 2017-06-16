using System;
using System.Collections.Generic;
using Talamoana.Domain.Core.Items;
using Talamoana.Domain.Core.Stats;

namespace Talamoana.Domain.Core.Modifiers
{
    /// <summary>
    ///     Modifier & values wrapper used in materialized items to represent actual modifiers.
    /// </summary>
    /// <seealso cref="Item" />
    public interface IMaterializedModifier
    {
        /// <summary>
        ///     The modifier that is being represented with values.
        /// </summary>
        IModifier Modifier { get; }

        /// <summary>
        ///     Stat values represented in the <see cref="IModifier">modifier</see>.
        /// </summary>
        IReadOnlyDictionary<IStat, int> Values { get; }
    }
}