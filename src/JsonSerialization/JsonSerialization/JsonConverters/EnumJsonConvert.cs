using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reflection;

namespace JsonSerialization
{
    /// <summary>
    /// 将枚举项为中文的枚举类型转换为Json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumJsonConvert : JsonConverter
    {
        public override bool CanConvert(Type objectType) => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // 若未定义，使用默认值
            if (null == reader.Value || !Enum.IsDefined(objectType, reader.Value))
                return Activator.CreateInstance(objectType);

            return Enum.Parse(objectType, reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
