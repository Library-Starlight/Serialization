using Newtonsoft.Json;
using ProtoBuf;
using ProtoBuf.Meta;
using ProtoBufSerialization_224.Serializers;
using System;
using System.IO;

namespace ProtoBufSerialization_224
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
        public int V5 { get; set; }
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Begin");
            Console.WriteLine();

            byte[] data;
            ValueDemo model;
            byte[] data1;
            MemoryStream ms;
            ProtoWriter writestate;
            var typeModel = RuntimeTypeModel.Create();

            // Bool
            ms = new MemoryStream();
            writestate = ProtoBuf.ProtoWriter.Create(ms, typeModel);
            ProtoWriter.WriteFieldHeader(3, WireType.Variant, writestate);
            ProtoWriter.WriteBoolean(true, writestate);
            writestate.Close();
            ms.Position = 0;

            data = ms.ToArray();
            model = new ValueDemo { Success = true };
            data1 = ProtoBufSerializer.Serialize(model);
            ms.Close();
            ms = null;

            Print("bool");

            // Int32
            ms = new MemoryStream();
            writestate = ProtoBuf.ProtoWriter.Create(ms, typeModel);
            ProtoWriter.WriteFieldHeader(5, WireType.Variant, writestate);
            ProtoWriter.WriteInt32(500, writestate);
            writestate.Close();
            ms.Position = 0;

            data = ms.ToArray();
            model = new ValueDemo { Value = 500 };
            data1 = ProtoBufSerializer.Serialize(model);
            ms.Close();
            ms = null;

            Print("int32");

            // Uint32
            ms = new MemoryStream();
            writestate = ProtoBuf.ProtoWriter.Create(ms, typeModel);
            ProtoWriter.WriteFieldHeader(4, WireType.Variant, writestate);
            ProtoWriter.WriteUInt32(UInt32.MaxValue, writestate);
            writestate.Close();
            ms.Position = 0;

            data = ms.ToArray();
            model = new ValueDemo { Values = UInt32.MaxValue };
            data1 = ProtoBufSerializer.Serialize(model);
            ms.Close();
            ms = null;

            Print("uint32");

            Console.WriteLine();
            Console.WriteLine($"End");

            // Int32
            

            void Print(string s)
            {
                Console.WriteLine($"{s} - wri: {BitConverter.ToString(data)}");
                Console.WriteLine($"{s} - ser: {BitConverter.ToString(data1)}");
            }
        }

        static void Introduce()
        {
            //var writer = new ProtoBuf.ProtoWriter();

            var typeModel = RuntimeTypeModel.Create();
            using var ms = new MemoryStream();
            var state = ProtoBuf.ProtoWriter.Create(ms, typeModel);
            //state.WriteFieldHeader(10, ProtoBuf.WireType.Fixed64);
            //state.WriteUInt64(10000);

            ProtoWriter.WriteFieldHeader(25, ProtoBuf.WireType.Variant, state);
            ProtoWriter.WriteBoolean(true, state);

            state.Close();

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
