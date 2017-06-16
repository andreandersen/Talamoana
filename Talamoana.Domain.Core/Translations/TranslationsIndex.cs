using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Murmur;

namespace Talamoana.Domain.Core.Translations
{
    public class TranslationsIndex
    {
        private static readonly HashAlgorithm Hash = MurmurHash.Create128(managed: false);
        
        private readonly ReadOnlyDictionary<byte[], Translation> _byHash;
        private readonly ReadOnlyDictionary<byte[], IReadOnlyList<Translation>> _byIdHash;
        private readonly ReadOnlyDictionary<byte[], IReadOnlyList<Translation>> _byTranslation;

        public TranslationsIndex(ITranslationReader reader)
        {
            var translations = reader.Read().ToList();

            _byHash = new ReadOnlyDictionary<byte[], Translation>(
                translations.ToDictionary(HashStatIds, p => p));

            _byIdHash = new ReadOnlyDictionary<byte[], IReadOnlyList<Translation>>(translations
                .SelectMany(p => p.StatIds.Select(e => new KeyValuePair<byte[], Translation>(HashString(e), p)))
                .GroupBy(p => p.Key)
                .ToDictionary(p => p.Key, p => (IReadOnlyList<Translation>) p.Select(q => q.Value).ToList()));

            _byTranslation = new ReadOnlyDictionary<byte[], IReadOnlyList<Translation>>(translations
                .Where(p => p.Translations.All(t => !string.IsNullOrEmpty(t.Translation)))
                .SelectMany(p => p.Translations.Select(
                    e => new KeyValuePair<byte[], Translation>(HashString(e.Translation), p)))
                .GroupBy(p => p.Key)
                .ToDictionary(p => p.Key, p => (IReadOnlyList<Translation>) p.Select(q => q.Value).ToList()));
        }

        public static byte[] HashStatIds(Translation item) => 
            HashString(string.Join("", item.StatIds.OrderBy(p => p)));

        public static byte[] HashString(string item) => 
            Hash.ComputeHash(Encoding.UTF8.GetBytes(item));
    }
}