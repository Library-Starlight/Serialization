using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerialization.JsonConverters
{
    /// <summary>
    /// 将Utc数字转换为<see cref="DateTime"/>
    /// </summary>
    public class UtcDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            => long.Parse(reader.Value.ToString()).UtcMillisecondsToTime();

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => writer.WriteValue(((DateTime)value).GetUtcMilliseconds());
    }
}
