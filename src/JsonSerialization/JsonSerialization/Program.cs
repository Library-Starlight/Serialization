using JsonSerialization.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializationEnum();

            Console.ReadLine();
        }

        #region 序列化枚举

        private static void SerializationEnum()
        {
            var settings = new Settings() { Options = Options.Value3 };
            var json = JsonConvert.SerializeObject(settings);

            Console.WriteLine(json);

            var model = JsonConvert.DeserializeObject<Settings>(json);
            Console.WriteLine(model.Options);
        }

        #endregion

        #region 特殊节点名序列化

        /// <summary>
        /// 特殊节点名序列化
        /// </summary>
        static void SpecialNodeNameSerialization()
        {
            var model = new TradeTrendModel()
            {
                value = new TradeTrendItemModel
                {
                    avgProcessTime = new List<int> { 1, 2, 3 },
                    avgStatus = new List<double> { 60.1, 70.5, 80.0 },
                    count = new List<int> { 845, 960, 632 },
                    time = new List<string> { "2017090600", "2017090601", "2017090602" },
                },
            };

            var json = JsonConvert.SerializeObject(model);

            Console.WriteLine(json);
        }

        #endregion

        #region 从字符串解析

        static void ParseFromString()
        {
            var s = "{\"longitude\":\"119.272696\",\"latitude\":\"26.128918\",\"province\":\"\",\"city\":\"\",\"district\":\"\",\"street\":\"\",\"streetNumber\":\"\",\"address\":\"\",\"addressDetail\":\"福建省福州市晋安区G1501(福州绕城高速)\"}";

            var jObj = JObject.Parse(s);

            Console.WriteLine(jObj["addressDetail"]);
        }

        #endregion

        #region 自定义时间转换

        private static void CustomDateTimeConverter()
        {
            PrintObject<Times>();
            //PrintObject<HycTaskResultRow>();
            return;

            var times = new Times();
            var json = JsonConvert.SerializeObject(times, Formatting.Indented);

            times = JsonConvert.DeserializeObject<Times>(json);

            Console.WriteLine(times.Fotmatter);
            Console.WriteLine(json);
        }

        #endregion

        #region 字符串枚举转换

        private static void StringToEnumConvert()
        {
            PrintObject<SampleEnumClass>();
        }

        #endregion

        #region 读取节点值

        /// <summary>
        /// 使用Linq to Json读取节点值
        /// </summary>
        private static async void ReadNodeValue()
        {
            using (var fs = new FileStream("Text/PackingSpaceResponse.json", FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs))
            {
                var json = await sr.ReadToEndAsync();

                var jObj = JObject.Parse(json);

                Console.WriteLine($"原文：");
                Console.WriteLine(jObj.ToString(Formatting.Indented));

                // 子节点

                Console.WriteLine($"serviceId：{jObj["serviceId"]}");
                Console.WriteLine($"dataItems：{jObj["dataItems"]}");

                Console.WriteLine($"dataItems[0]：{jObj["dataItems"][0]}");
                Console.WriteLine($"dataItems[0].objectId：{jObj["dataItems"][0]["operateType"]}");

                var wrongNode = jObj["fsdaad"];
                Console.WriteLine(wrongNode == null);
                Console.WriteLine(wrongNode.ToString());
            }
        }

        #endregion

        #region 将时间序列化为指定格式

        private static void SerializeEnglishDate()
        {
            var iso = DateFormatHandling.IsoDateFormat;
            var msc = DateFormatHandling.MicrosoftDateFormat;

            //PrintObject<HycSearchTaskResultResponse>();

            PrintObject<HycTaskResultData>();
        }

        private static async void SerializeDateTimeInSpecificFormat()
        {
            PrintObject<GarbageBinStatus>();
        }

        #endregion

        #region 序列化 / 反序列化嵌套的泛型类型

        /// <summary>
        /// 序列化嵌套泛型类型
        /// </summary>
        private static void SerializeNestGenericModel()
        {
            var requestBody = new QueryParkSpace()
            {
                parkCodes = "100501",
            };

            var request = new JhtCloudRequest<QueryParkSpace>()
            {
                attributes = requestBody,
            };

            // 序列化
            var json = JsonConvert.SerializeObject(request);
            Console.WriteLine(json);

            // 反序列化
            request = JsonConvert.DeserializeObject<JhtCloudRequest<QueryParkSpace>>(json);

            Console.WriteLine($"serviceId: {request.serviceId}");
            Console.WriteLine($"requestType: {request.requestType}");
            Console.WriteLine($"packCodes: {request.attributes.parkCodes}");
        }

        /// <summary>
        /// 反序列化嵌套泛型类型
        /// </summary>
        private static void DeserializeNestGenericModel()
        {
            using (var fs = new FileStream("Text/PackingSpaceResponse.json", FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs, Encoding.GetEncoding("GBK")))
            {
                var json = sr.ReadToEnd();
                var response = JsonConvert.DeserializeObject<JhtCloudResponse<QueryParkSpaceResponse>>(json);

                Console.WriteLine(response.resultCode);
                Console.WriteLine(response.message);
                Console.WriteLine(response.dataItems.First().attributes.restSpace);
                Console.WriteLine(response.dataItems.First().attributes.totalSpace);
            }
        }

        #endregion

        #region 工具方法

        private static async void PrintObject<T>()
        {
            using (var fs = new FileStream($"Text/{typeof(T).Name}.json", FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs, Encoding.GetEncoding("GBK")))
            {
                var settings = new JsonSerializerSettings() { Culture = new System.Globalization.CultureInfo("zh-cn") };

                var json = await sr.ReadToEndAsync();
                var obj = JsonConvert.DeserializeObject<T>(json);

                Console.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
            }
        }

        #endregion
    }
}
