using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private readonly List<MaterializedModifier> _implicits;

        private string _name = string.Empty;

        private ItemRarity _rarity;

        public Item(IBaseItem baseItem, int itemLevel, ItemRarity rarity = ItemRarity.Normal)
        {
            if (rarity == ItemRarity.Unique)
                throw new InvalidOperationException("Unique rarity not supported by this constructor");

            Base = baseItem;
            ItemLevel = itemLevel;
            Rarity = rarity;

            Explicits = new List<MaterializedModifier>();
            _implicits = baseItem.Implicits
                .Select(p => p.Materialize(
                    p.Stats.ToDictionary(e => e.Stat, e => Convert.ToInt32((e.Min + e.Max) / 2d)))).ToList();

            //Tags = new List<short>(Base.Tags.Select(TagWeight.GetTagIdentifier));
            //Tags = Base.Tags.ToDictionary(p => TagWeight.GetTagIdentifier(p), _ => new short());
            Tags = new List<short>(Base.Tags.Select(TagWeight.GetTagIdentifier).ToList());

        }

        public Item(IBaseItem baseItem, int itemLevel, ItemRarity rarity, List<MaterializedModifier> implicits,
            List<MaterializedModifier> explicits, string nameOverride = null)
        {
            Base = baseItem;
            ItemLevel = itemLevel;
            Rarity = rarity;

            Explicits = new List<MaterializedModifier>(explicits);
            _implicits = new List<MaterializedModifier>(implicits);
            _name = nameOverride;
        }

        public string Name
        {
            get => DetermineName();
            private set => _name = value;
        }

        public int ItemLevel { get; }

        public bool IsCorrupted { get; private set; }

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
        /// <seealso cref="MaterializedModifier" />
        public List<MaterializedModifier> Explicits { get; private set; }

        public IReadOnlyCollection<MaterializedModifier> Implicits => _implicits;

        /// <summary>
        ///     All the tags that this item has based on the modifiers + base item tags
        /// </summary>
        public readonly List<short> Tags;

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

        public void Reset()
        {
            Explicits.Clear();
            _name = string.Empty;

            Rarity = ItemRarity.Normal;
            IsCorrupted = false;
        }

        public void ClearExplicitModifiers()
        {
            Explicits = new List<MaterializedModifier>();
            //Tags = new List<string>(Base.Tags);
        }

        public void AddExplicitModifier(MaterializedModifier modifier)
        {
            if (IsCorrupted)
                throw new InvalidOperationException("Item is corrupted");

            if (modifier.Modifier.GenerationType != GenerationType.Prefix &&
                modifier.Modifier.GenerationType != GenerationType.Suffix)
                throw new InvalidOperationException("Invalid modifier type provided");

            if (Explicits.Any(p => p.Modifier.Equals(modifier.Modifier)))
                throw new ModifierAlreadyInItemException();

            Explicits.Add(modifier);
            //_tags.InsertRange(0, modifier.Modifier.AddsTags);
        }

        public void ReplaceImplicitModifier(MaterializedModifier modifier)
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