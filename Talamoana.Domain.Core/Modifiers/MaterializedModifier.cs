using System;
using System.Collections.Generic;
using System.Linq;
using Talamoana.Domain.Core.Modifiers.Exceptions;
using Talamoana.Domain.Core.Stats;

namespace Talamoana.Domain.Core.Modifiers
{
    /// <inheritdoc />
    public class MaterializedModifier // : IMaterializedModifier
    {
        private readonly Dictionary<Stat, int> _values;

        /// <summary>
        ///     Materializes a Modifier with values, which is used on materialized items.
        /// </summary>
        /// <param name="modifier"><see cref="Modifier">Modifier</see> to materialize</param>
        /// <param name="values">Values that are being set. Must meet the stats used in the modifier</param>
        /// <returns>
        ///     <see cref="MaterializedModifier">Materialized modifier</see>
        /// </returns>
        /// <exception cref="ValuesDoNotMatchModifierException">
        ///     Thrown if <paramref name="values" /> don't match
        ///     <paramref name="modifier" /> stats
        /// </exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="modifier" /> or <paramref name="values" /> is null</exception>
        internal MaterializedModifier(Modifier modifier, Dictionary<Stat, int> values)
        {
            if (modifier.Stats.Count != values.Count ||
                !modifier.Stats.All(p => values.Any(c => c.Key.Id.Equals(p.Stat.Id))))
                throw new ValuesDoNotMatchModifierException();

            Modifier = modifier; // ?? throw new ArgumentNullException(nameof(modifier));
            _values = values ?? throw new ArgumentNullException(nameof(modifier));
        }

        /// <inheritdoc />
        public Modifier Modifier { get; }

        /// <inheritdoc />
        public Dictionary<Stat, int> Values => _values;
        
        public override string ToString() => Modifier.Id;
    }
}