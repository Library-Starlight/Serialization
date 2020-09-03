using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinarySerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var data = Serialize(list);

            Console.WriteLine(BitConverter.ToString(data));
            Console.WriteLine();

            var clone = Deserialize<List<int>>(data);
            Console.WriteLine(JsonConvert.SerializeObject(clone));
        }

        private static byte[] Serialize(object obj)
        {
            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();
            formatter.Serialize(stream, obj);

            stream.Position = 0;

            return stream.ToArray();
        }

        private static T Deserialize<T>(byte[] data)
        {
            var formatter = new BinaryFormatter();
            var stream = new MemoryStream(data);
            return (T)formatter.Deserialize(stream);
        }
    }
}
