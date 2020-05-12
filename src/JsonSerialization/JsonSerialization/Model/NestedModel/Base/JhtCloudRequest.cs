using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerialization
{
    /// <summary>
    /// 捷慧通云平台请求参数基类
    /// </summary>
    public class JhtCloudRequest
    { }

    /// <summary>
    /// 捷慧通云平台请求
    /// </summary>
    public class JhtCloudRequest<T>
        where T : JhtCloudRequest
    {
        /// <summary>
        /// 服务标识
        /// </summary>
        public string serviceId { get; set; }

        /// <summary>
        /// 请求类型
        /// </summary>
        public string requestType { get; set; }

        /// <summary>
        /// 请求数据体
        /// </summary>
        public T attributes { get; set; }
    }
}
