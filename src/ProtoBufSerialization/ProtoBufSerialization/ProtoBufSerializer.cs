using ProtoBuf;
using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProtoBufSerialization
{
    public static class ProtoBufSerializer
    {
        /// <summary>
        /// 类型模型
        /// </summary>
        private readonly static RuntimeTypeModel _typeModel = RuntimeTypeModel.Create();

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static byte[] Serialize<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                // 序列化到内存中
                Serializer.Serialize(ms, obj);

                // 读取到字节数组
                ms.Position = 0;
                var data = new byte[ms.Length];
                ms.Read(data, 0, data.Length);

                return data;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">二进制数据</param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] data)
        {
            using (var ms = new MemoryStream())
            {
                // 写入到内存中
                ms.Write(data, 0, data.Length);

                // 反序列化
                ms.Position = 0;
                var value = Serializer.Deserialize<T>(ms);
                return value;
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="order">序号</param>
        /// <returns></returns>
        public static byte[] GetData(int value, int order)
        {
            using var ms = new MemoryStream();
            using var writer = ProtoWriter.State.Create(ms, _typeModel);

            // 写入数据
            writer.WriteFieldHeader(order, WireType.Varint);
            writer.WriteInt32(value);

            // 释放缓冲区
            writer.Flush();

            ms.Position = 0;
            var data = ms.ToArray();

            return data;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="order">序号</param>
        /// <returns></returns>
        public static byte[] GetData(uint value, int order)
        {
            using var ms = new MemoryStream();
            using var writer = ProtoWriter.State.Create(ms, _typeModel);

            // 写入数据
            writer.WriteFieldHeader(order, WireType.Varint);
            writer.WriteUInt32(value);

            // 释放缓冲区
            writer.Flush();

            ms.Position = 0;
            var data = ms.ToArray();

            return data;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="order">序号</param>
        /// <returns></returns>
        public static byte[] GetData(bool value, int order)
        {
            using var ms = new MemoryStream();
            using var writer = ProtoWriter.State.Create(ms, _typeModel);

            // 写入数据
            writer.WriteFieldHeader(order, WireType.Varint);
            writer.WriteBoolean(value);

            // 释放缓冲区
            writer.Flush();

            ms.Position = 0;
            var data = ms.ToArray();

            return data;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="order">序号</param>
        /// <returns></returns>
        public static byte[] GetData(double value, int order)
        {
            using var ms = new MemoryStream();
            using var writer = ProtoWriter.State.Create(ms, _typeModel);

            // 写入数据
            writer.WriteFieldHeader(order, WireType.Varint);
            writer.WriteDouble(value);

            // 释放缓冲区
            writer.Flush();

            ms.Position = 0;
            var data = ms.ToArray();

            return data;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="order">序号</param>
        /// <returns></returns>
        public static byte[] GetData(string value, int order)
        {
            using (var ms = new MemoryStream())
            {
                var writer = ProtoWriter.State.Create(ms, _typeModel);

                // 写入数据
                writer.WriteFieldHeader(order, WireType.String);
                writer.WriteString(value);

                // 释放缓冲区
                writer.Flush();

                ms.Position = 0;
                var data = ms.ToArray();

                return data;
            }
        }
    }
}
