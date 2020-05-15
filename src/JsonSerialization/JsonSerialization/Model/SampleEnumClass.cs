using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerialization
{
    public class SampleEnumClass
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public SampleEnum Value { get; set; }
    }

    public enum SampleEnum
    {
        value1 = 1,
        value2 = 2,
    }
}