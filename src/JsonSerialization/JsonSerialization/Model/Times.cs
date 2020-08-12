using JsonSerialization.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace JsonSerialization
{
    public class Times
    {
        public DateTime Default { get; set; } = DateTime.Now;

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime ISO { get; set; } = DateTime.Now;

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Unix { get; set; } = DateTime.Now;

        [JsonConverter(typeof(EnglishDateConverter))]
        public DateTime Fotmatter { get; set; } = DateTime.Now.AddMonths(-10);

        [JsonConverter(typeof(UtcDateTimeConverter))]
        public DateTime UtcTime { get; set; } = DateTime.Now;
    }
}