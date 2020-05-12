using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializeNestGenericModel();
            DeserializeNestGenericModel();
        }

        /// <summary>
        /// 序列化嵌套泛型类型
        /// </summary>
        private static void SerializeNestGenericModel()
        {
            var requestBody = new ParkingSpaceRequest()
            {
                parkCodes = "100501",
            };

            var request = new JhtCloudRequest<ParkingSpaceRequest>()
            {
                serviceId = "S1054",
                requestType = "DATA",
                attributes = requestBody,
            };

            // 序列化
            var json = JsonConvert.SerializeObject(request);
            Console.WriteLine(json);

            // 反序列化
            request = JsonConvert.DeserializeObject<JhtCloudRequest<ParkingSpaceRequest>>(json);

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
                var response = JsonConvert.DeserializeObject<JhtCloudResponse<ParkingSpaceResponse>>(json);

                Console.WriteLine(response.resultCode);
                Console.WriteLine(response.message);
                Console.WriteLine(response.dataItems.First().attributes.restSpace);
                Console.WriteLine(response.dataItems.First().attributes.totalSpace);
            }
        }
    }
}
