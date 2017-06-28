using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Talamoana.Domain.Core.Translations
{
    /// <summary>
    ///     Based on RePoE's script to extract .data using PyPoE into .json files
    /// </summary>
    public class JsonTranslationsReader : ITranslationReader
    {
        private readonly string _file;

        public JsonTranslationsReader(string sourceFile = "Data\\stat_translations.json")
        {
            _file = sourceFile;
        }

        public List<Translation> Read()
        {
            var json = File.ReadAllText(_file);
            var objs = JsonConvert.DeserializeObject<List<TranslationObject>>(json);

            var items = objs.Select(o =>
            {
                var strings = o.English.Select(t =>
                {
                    var conditions = t.condition.Select(c =>
                        new TranslationCondition(c.min, c.max)).ToList();
                    return new TranslationString(t.@string, conditions, t.format.ToList(), t.index_handlers.Select(c => c.ToList()).ToList());
                });

                return new Translation(o.ids.ToList(), strings.ToList());
            });

            return items.ToList();
        }

        #pragma warning disable 649
        // ReSharper disable ClassNeverInstantiated.Local
        // ReSharper disable InconsistentNaming
        private class TranslationObject
        {
            public TranslationObjectParams[] English;
            public string[] ids;
        }

        private class TranslationObjectParams
        {
            public TranslationConditionObject[] condition;
            public string[] format;
            public string[][] index_handlers;
            public string @string;
        }

        private class TranslationConditionObject
        {
            public int? max;
            public int? min;
        }
        // ReSharper restore InconsistentNaming
    }
}