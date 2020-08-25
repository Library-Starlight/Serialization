using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProtoBufSerialization
{

    public static class ProtoBufHelper
    {
        /// <summary>
        /// 支持的数据类型
        /// </summary>
        private enum DataType
        {
            Int32 = 1,
            Uint32 = 2,
            Bool = 3,
        }

        /// <summary>
        /// 获取ProtoBuf序列化值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="type">数据类型</param>
        /// <param name="order">ProtoBuf序列号</param>
        /// <returns></returns>
        public static byte[] GetData(object value, string type, int order)
        {
            if (value == null)
                return null;

            // 获取数据类型
            if (!Enum.TryParse<DataType>(type, true, out var dataType))
                return null;

            // 获取序列化数据
            switch (dataType)
            {
                case DataType.Int32:
                    return FromInt32(value, order);
                case DataType.Uint32:
                    return FromUint32(value, order);
                case DataType.Bool:
                    return FromBool(value, order);
                default:
                    return null;
            }
        }

        private static byte[] FromInt32(object value, int order)
        {
            if (value is int v1 || int.TryParse(value.ToString(), out v1))
                return ProtoBufSerializer.GetData(v1, order);
            return null;
        }

        private static byte[] FromUint32(object value, int order)
        {
            if (value is uint v1 || uint.TryParse(value.ToString(), out v1))
                return ProtoBufSerializer.GetData(v1, order);
            return null;
        }

        private static byte[] FromBool(object value, int order)
        {
            if (value is bool b || bool.TryParse(value.ToString(), out b))
                return ProtoBufSerializer.GetData(b, order);
            return null;
        }
    }

}
