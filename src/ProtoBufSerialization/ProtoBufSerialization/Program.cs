using Newtonsoft.Json;
using ProtoBuf;
using ProtoBuf.Meta;
using System;
using System.IO;

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
    }

    class Program
    {
        static void Main(string[] args)
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
