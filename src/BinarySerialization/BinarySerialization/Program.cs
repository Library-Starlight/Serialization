#define FS
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
            var stream = Serialize(list);

            var clone = Deserialize<List<int>>(stream);
            Console.WriteLine(JsonConvert.SerializeObject(clone));
        }

        private static Stream Serialize(object obj)
        {
            var formatter = new BinaryFormatter();
#if FS
            var stream = new FileStream("model.dat", FileMode.Create, FileAccess.ReadWrite);
#else
            var stream = new MemoryStream();
#endif
            formatter.Serialize(stream, obj);

            stream.Position = 0;
            return stream;
        }

        private static T Deserialize<T>(Stream stream)
        {
            var formatter = new BinaryFormatter();
            var obj = formatter.Deserialize(stream);
            return (T)obj;
        }
    }
}
