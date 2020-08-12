using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// <see cref="DateTime"/> 的扩展方法
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Utc时间的零点
        /// </summary>
        private static readonly DateTime _utcZero = new DateTime(1970, 1, 1);

        /// <summary>
        /// 获取时间的Utc整数值
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public static long GetUtcMilliseconds(this DateTime dateTime)
        {
            return (long)(dateTime - _utcZero).TotalMilliseconds;
        }

        /// <summary>
        /// 将Utc整数值转换为时间
        /// </summary>
        /// <param name="value">需要转换为时间的Utc整数值</param>
        /// <returns></returns>
        public static DateTime UtcMillisecondsToTime(this long value)
        {
            return _utcZero.AddMilliseconds(value);
        }
    }
}
