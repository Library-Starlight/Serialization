using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerialization
{
    /// <summary>
    /// 空车位查询请求
    /// </summary>
    public class ParkingSpaceRequest : JhtCloudRequest
    {
        /// <summary>
        /// 停车场编号,多个编号之间以逗号（“,”）分隔
        /// </summary>
        public string parkCodes { get; set; }
    }
}
