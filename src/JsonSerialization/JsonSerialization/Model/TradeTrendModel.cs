using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerialization.Model
{
    public class TradeTrendModel
    {
        [JsonProperty("appName|TIP")]
        public TradeTrendItemModel value { get; set; }
    }

    public class TradeTrendItemModel
    {
        public List<double> avgStatus { get; set; }
        public List<string> time { get; set; }
        public List<int> count { get; set; }
        public List<int> avgProcessTime { get; set; }
    }
}
