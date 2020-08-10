using ProtoBuf.Meta;
using System;
using System.IO;
using static ProtoBuf.ProtoWriter;

namespace ProtoBufSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            //var writer = new ProtoBuf.ProtoWriter();

            var typeModel = RuntimeTypeModel.Create();
            using var ms = new MemoryStream();
            var state = State.Create(ms, typeModel);
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
