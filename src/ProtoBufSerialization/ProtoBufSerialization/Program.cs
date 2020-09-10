using Newtonsoft.Json;
using ProtoBuf;
using ProtoBuf.Meta;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace ProtoBufSerialization
{

    public enum ModelState
    {
        V1 = 1,
        V2 = 2,
        V3 = 3,
    }

    [ProtoContract]
    public class DemoModel
    {
        [ProtoMember(1)]
        public ModelState Value { get; set; }
        [ProtoMember(2)]
        public string S { get; set; }
        [ProtoMember(3)]
        public uint V1 { get; set; }
        [ProtoMember(4)]
        public bool Result { get; set; }
        [ProtoMember(5)]
        public string Str { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Read();
        }

        static void Read()
        {
            var data = FromBool(1, 10);
            Print();
            data = FromBool(0, 10);
            Print();

            //GetUInt32ofPB(7, 96);

            void Print()
            {
                Console.WriteLine(BitConverter.ToString(data));
            }
        }

        static void MemoryWriter()
        {
            var typeModel = RuntimeTypeModel.Create();

            var ms = new MemoryStream();
            var writer = ProtoWriter.State.Create(ms, typeModel);

            // Int - 5, 500
            writer.WriteFieldHeader(5, WireType.Varint);
            writer.WriteInt32(500);
            writer.Flush();
            ms.Position = 0;
            var data = ms.ToArray();
            ms.Close();
            ms.Dispose();
            ms = null;
            Print(1);

            var value1 = new ValueDemo() { Value = 500 };
            data = ProtoBufSerializer.Serialize(value1);
            Print(2);

            data = ProtoBufHelper.GetData(500, "int32", 5);
            Print(3);

            // UInt32 - 4, Uint.MaxValue
            ms = new MemoryStream();
            writer = ProtoWriter.State.Create(ms, typeModel);
            writer.WriteFieldHeader(4, WireType.Varint);
            writer.WriteUInt32(uint.MaxValue);
            writer.Flush();
            ms.Position = 0;
            data = ms.ToArray();
            ms.Close();
            ms.Dispose();
            ms = null;
            Print(4);

            value1 = new ValueDemo() { Values = UInt32.MaxValue };
            data = ProtoBufSerializer.Serialize(value1);
            Print(5);

            data = ProtoBufHelper.GetData(UInt32.MaxValue, "uint32", 4);
            Print(6);

            // Boolean, 300 - true
            ms = new MemoryStream();
            writer = ProtoWriter.State.Create(ms, typeModel);
            writer.WriteFieldHeader(3, WireType.Varint);
            writer.WriteBoolean(true);
            writer.Flush();
            ms.Position = 0;
            data = ms.ToArray();
            ms.Close();
            ms.Dispose();
            ms = null;
            Print(7);

            value1 = new ValueDemo { Success = true };
            data = ProtoBufSerializer.Serialize(value1);
            Print(8);

            data = ProtoBufHelper.GetData(true, "bool", 3);
            Print(9);

            // String
            var s = "Hello World!哈罗！";
            ms = new MemoryStream();
            writer = ProtoWriter.State.Create(ms, typeModel);
            writer.WriteFieldHeader(6, WireType.String);
            writer.WriteString(s);
            writer.Flush();
            ms.Position = 0;
            data = ms.ToArray();
            ms.Close();
            ms.Dispose();
            ms = null;
            Print(10);

            value1 = new ValueDemo { Str = s };
            data = ProtoBufSerializer.Serialize(value1);
            Print(11);

            data = ProtoBufHelper.GetData(s, "string", 6);
            Print(12);

            // double
            var d = 105.5D;
            ms = new MemoryStream();
            writer = ProtoWriter.State.Create(ms, typeModel);
            writer.WriteFieldHeader(7, WireType.Varint);
            writer.WriteDouble(d);
            writer.Flush();
            ms.Position = 0;
            data = ms.ToArray();
            ms.Close();
            ms.Dispose();
            ms = null;
            Print(13);

            value1 = new ValueDemo { Measure = d };
            data = ProtoBufSerializer.Serialize(value1);
            Print(14);

            data = ProtoBufHelper.GetData(s, "double", 7);
            Print(15);

            void Print(int i)
            {
                Console.WriteLine($"{i}: {BitConverter.ToString(data)}");
            }
        }

        private static void GetUInt32ofPB(int order, uint value)
        {
            var data1 = FromUint32(19.1D, -1, 7);
            Console.WriteLine(BitConverter.ToString(data1));
            return;

            // [2020-09-07 02:10:36] 上传属性，属性值: 9.6, 属性名: 目标湿度, 设备号: 53, 测点号: 18, 测点类型: C
            // [2020-09-07 02:10:36] 上传属性, 结果: 成功(0), homeaddr: 389, 服务Id: 260608, iid: 7, 属性二进制值: 0x3809
            // [2020-09-07 02:52:48] 上传属性，属性值: 12, 属性名: 当前湿度, 设备号: 53, 测点号: 14, 测点类型: C
            // [2020-09-07 02:52:48] 上传属性, 结果: 成功(0), homeaddr: 390, 服务Id: 260352, iid: 7, 属性二进制值: 0x3800
            // [2020-09-07 02:52:48] 上传属性，属性值: 15.1, 属性名: 当前湿度, 设备号: 53, 测点号: 17, 测点类型: C
            // [2020-09-07 02:52:48] 上传属性, 结果: 成功(0), homeaddr: 390, 服务Id: 260608, iid: 6, 属性二进制值: 0x3000
            // [2020-09-07 02:52:48] 上传属性，属性值: 19.1, 属性名: 目标湿度, 设备号: 53, 测点号: 18, 测点类型: C
            // [2020-09-07 02:52:48] 上传属性, 结果: 成功(0), homeaddr: 390, 服务Id: 260608, iid: 7, 属性二进制值: 0x3800
            // [2020-09-07 02:52:48] 上传属性，属性值: 16.4, 属性名: 目标湿度, 设备号: 53, 测点号: 22, 测点类型: C
            // [2020-09-07 02:52:48] 上传属性, 结果: 成功(0), homeaddr: 390, 服务Id: 256512, iid: 12, 属性二进制值: 0x6000
            // [2020-09-07 02:52:48] 上传属性，属性值: 7, 属性名: 频率控制, 设备号: 53, 测点号: 31, 测点类型: C
            // [2020-09-07 02:52:48] 上传属性, 结果: 成功(0), homeaddr: 390, 服务Id: 256512, iid: 7, 属性二进制值: 0x3800
            // [2020-09-07 02:52:48] 上传属性，属性值: 17.5, 属性名: 频率反馈, 设备号: 53, 测点号: 32, 测点类型: C
            // [2020-09-07 02:52:48] 上传属性, 结果: 成功(0), homeaddr: 390, 服务Id: 256512, iid: 8, 属性二进制值: 0x4000
            // [2020-09-07 02:52:48] 上传属性，属性值: False, 属性名: 故障状态, 设备号: 53, 测点号: 28, 测点类型: X
            // [2020-09-07 02:52:48] 上传属性, 结果: 成功(0), homeaddr: 390, 服务Id: 256512, iid: 3, 属性二进制值: 0x1800
            // [2020-09-07 02:52:48] 上传属性，属性值: False, 属性名: 中效滤网状态, 设备号: 53, 测点号: 5, 测点类型: X
            // [2020-09-07 02:52:48] 上传属性, 结果: 成功(0), homeaddr: 390, 服务Id: 256768, iid: 2, 属性二进制值: 0x1000

            var typeModel = RuntimeTypeModel.Create();
            var ms = new MemoryStream();
            var writer = ProtoWriter.State.Create(ms, typeModel);
            writer.WriteFieldHeader(order, WireType.Varint);
            writer.WriteUInt32(value);
            writer.Flush();
            ms.Position = 0;
            var data = ms.ToArray();
            ms.Close();
            ms.Dispose();
            ms = null;

            Print(order);

            void Print(int i)
            {
                Console.WriteLine($"Order：{i.ToString()}, Value: {value.ToString()}, Data: {BitConverter.ToString(data)}");
            }
        }

        /// <summary>
        /// 获取无符号整型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="precision">精度</param>
        /// <param name="order">ProtoBuf序列号</param>
        /// <returns></returns>
        private static byte[] FromUint32(object value, int precision, int order)
        {
            uint v = default;
            if (value is double d || double.TryParse(value.ToString(), out d))
            {
                if (precision != 0)
                    v = (uint)(d * Math.Pow(10, -precision));
            }
            else if (value is uint v1 || uint.TryParse(value.ToString(), out v1))
            {
                if (precision != 0)
                    v = (uint)(v1 * Math.Pow(10, -precision));
            }
            else
                return null;

            return ProtoBufSerializer.GetData(v, order);
        }

        /// <summary>
        /// 获取布尔类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="order">ProtoBuf序列号</param>
        /// <returns></returns>
        private static byte[] FromBool(object value, int order)
        {
            var sValue = value.ToString();
            if (int.TryParse(sValue, out var i))
            {
                if (i == 1 || i == 0)
                    return ProtoBufSerializer.GetData(i == 1, order);
                return null;
            }
            if (value is bool b || bool.TryParse(sValue, out b))
                return ProtoBufSerializer.GetData(b, order);

            return null;
        }

        [ProtoContract]
        public class ValueDemo
        {
            [ProtoMember(3)]
            public bool Success { get; set; }

            [ProtoMember(4)]
            public uint Values { get; set; }

            [ProtoMember(5)]
            public int Value { get; set; }

            [ProtoMember(6)]
            public string Str { get; set; }
            [ProtoMember(7)]
            public double Measure { get; set; }
        }


        static void Test()
        {
            Console.WriteLine($"Begin");

            // 数据类型列举
            var typeModel = RuntimeTypeModel.Create();

            using (var ms0 = new MemoryStream())
            {
                ms0.Write(new byte[] { 0x18, 0xE8, 0x07 });
                ms0.Position = 0;

                var readState = ProtoBuf.ProtoReader.State.Create(ms0, typeModel);
                var tag = readState.ReadFieldHeader();
                var value = readState.ReadUInt32();
                Console.WriteLine($"tag: {tag.ToString()}, value: {value.ToString()}");
                //readState.Read
            }

            using var ms = new MemoryStream();
            var writestate = ProtoBuf.ProtoWriter.State.Create(ms, typeModel);

            //state.WriteFieldHeader(1, WireType.Fixed32);

            //state.WriteFieldHeader(1, WireType.Fixed64);
            //state.WriteInt32(10);

            //writestate.WriteFieldHeader(2, WireType.String);
            //writestate.WriteString("Hello World!");

            //writestate.WriteFieldHeader(2, WireType.Varint);
            writestate.WriteFieldHeader(4, WireType.Varint);
            writestate.WriteBoolean(true);

            //writestate.WriteFieldHeader(3, WireType.Varint);
            //writestate.WriteUInt32(1000);

            //state.WriteInt32Varint(1, 10);
            writestate.Flush();

            ms.Position = 0;
            var data = ms.ToArray();
            Console.WriteLine(BitConverter.ToString(data));

            var model = Serializer.Deserialize<DemoModel>(ms);
            Console.WriteLine(JsonConvert.SerializeObject(model));

            var m = new DemoModel
            {
                //Value = ModelState.V1,
                //S = "Hello World!",
                //V1 = 1000,
                Result = true,
            };
            using (var ms1 = new MemoryStream())
            {
                Serializer.Serialize(ms1, m);
                ms1.Position = 0;
                var data1 = ms1.ToArray();
                Console.WriteLine(BitConverter.ToString(data1));
            }


            //var def = RuntimeTypeModel.Default;

            //var type = typeof(int);
            //var meta = def[type];
            //Console.WriteLine(meta);
            Console.WriteLine($"End");
        }

        static void Introduce()
        {
            //var writer = new ProtoBuf.ProtoWriter();

            var typeModel = RuntimeTypeModel.Create();
            using var ms = new MemoryStream();
            var state = ProtoBuf.ProtoWriter.State.Create(ms, typeModel);
            //state.WriteFieldHeader(10, ProtoBuf.WireType.Fixed64);
            //state.WriteUInt64(10000);

            state.WriteFieldHeader(25, ProtoBuf.WireType.Varint);
            state.WriteBoolean(true);

            state.Flush();

            ms.Position = 0;
            var data = ms.ToArray();
            Console.WriteLine(BitConverter.ToString(data));

            //MessageParser parser = 
            //IMessage message = parser.ParseFrom(new byte[] { 0xC8, 0x01, 0x01 });

            //var descriptor = message.Descriptor;
            //foreach (var field in descriptor.Fields.InDeclarationOrder())
            //{
            //    Console.WriteLine(
            //        "Field {0} ({1}): {2}",
            //        field.FieldNumber,
            //        field.Name,
            //        field.Accessor.GetValue(message));
            //}
        }
    }
}
