using System;
using System.Collections.Generic;
using System.Linq;
using Talamoana.Domain.Core.Items.Base;
using Talamoana.Domain.Core.Modifiers;
using Talamoana.Domain.Core.Modifiers.Exceptions;

namespace Talamoana.Domain.Core.Items
{
    /// <summary>
    ///     Representation of a materialized item based on a Base Item, containing
    ///     domain logic and constraints.
    /// </summary>
    /// <remarks>
    ///     This type is not thread safe.
    /// </remarks>
    public class Item
    {
        // Explicit modifiers applied to this particular item
        private readonly List<IMaterializedModifier> _explicits;
        private readonly List<IMaterializedModifier> _implicits;

        public Item(IBaseItem baseItem, int itemLevel, ItemRarity rarity = ItemRarity.Normal)
        {
            if (rarity == ItemRarity.Unique)
                throw new InvalidOperationException("Unique rarity not supported by this constructor");

            Base = baseItem;
            ItemLevel = itemLevel;
            Rarity = rarity;

            _explicits = new List<IMaterializedModifier>();
            _implicits = baseItem.Implicits.Select(p => p.Materialize(p.Stats.ToDictionary(e => e.Stat, e => Convert.ToInt32((e.Min + e.Max) / 2d)))).Cast<IMaterializedModifier>().ToList();
        }

        public Item(IBaseItem baseItem, int itemLevel, ItemRarity rarity, IEnumerable<IMaterializedModifier> implicits,
            IEnumerable<IMaterializedModifier> explicits, string nameOverride = null)
        {
            Base = baseItem;
            ItemLevel = itemLevel;
            Rarity = rarity;

            _explicits = explicits.ToList();
            _implicits = implicits.ToList();
            _name = nameOverride;
        }

        private string _name = string.Empty;

        public string Name
        {
            get => DetermineName();
            private set => _name = value;
        }

        private string DetermineName()
        {
            if (!string.IsNullOrEmpty(_name))
                return _name;

            if (Rarity == ItemRarity.Magic)
            {
                var prefix = Explicits.FirstOrDefault(p => p.Modifier.GenerationType == GenerationType.Prefix)
                    ?.Modifier.Name;

                var suffix = Explicits.FirstOrDefault(p => p.Modifier.GenerationType == GenerationType.Suffix)
                    ?.Modifier.Name;

                _name = $"{prefix} {Base.Name} {suffix}".Trim();
            }
            else if (Rarity == ItemRarity.Rare)
            {
                _name = RareNames.GenerateRareName(this);
            }

            return _name;
        }

        public int ItemLevel { get; }

        public bool IsCorrupted { get; private set; }

        private ItemRarity _rarity;
        public ItemRarity Rarity
        {
            get => _rarity;
            set
            {
                if (_rarity == value) return;
                _name = string.Empty;
                _rarity = value;
            }
        }

        /// <inheritdoc />
        public IBaseItem Base { get; }

        /// <summary>
        ///     Explicit modifiers applied to the item
        /// </summary>
        /// <seealso cref="IMaterializedModifier" />
        public IReadOnlyList<IMaterializedModifier> Explicits => _explicits;

        public IReadOnlyCollection<IMaterializedModifier> Implicits => _implicits;

        /// <summary>
        ///     All the tags that this item has based on the modifiers + base item tags
        /// </summary>
        public IEnumerable<string> Tags =>
            Explicits.SelectMany(c => c.Modifier.AddsTags).Concat(Base.Tags);

        public void Reset()
        {
            _explicits.Clear();
            _name = string.Empty;

            Rarity = ItemRarity.Normal;
            IsCorrupted = false;
        }

        public void ClearExplicitModifiers() => _explicits.Clear();

        public void AddExplicitModifier(IMaterializedModifier modifier)
        {
            if (IsCorrupted)
                throw new InvalidOperationException("Item is corrupted");

            if (modifier.Modifier.GenerationType != GenerationType.Prefix &&
                modifier.Modifier.GenerationType != GenerationType.Suffix)
                throw new InvalidOperationException("Invalid modifier type provided");

            if (_explicits.Any(p => p.Modifier.Equals(modifier.Modifier)))
                throw new ModifierAlreadyInItemException();

            _explicits.Add(modifier);
        }

        public void ReplaceImplicitModifier(IMaterializedModifier modifier)
        {
            if (IsCorrupted)
                throw new InvalidOperationException("Item is corrupted");

            if (modifier.Modifier.GenerationType != GenerationType.Corrupted &&
                modifier.Modifier.GenerationType != GenerationType.Enchantment)
                throw new InvalidOperationException("Invalid modifier type provided");

            _implicits.Clear();
            _implicits.Add(modifier);

            if (modifier.Modifier.GenerationType == GenerationType.Corrupted)
                IsCorrupted = true;
        }
    }
}