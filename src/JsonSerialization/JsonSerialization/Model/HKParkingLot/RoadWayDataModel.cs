using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerialization.Model
{
    public class RoadWayDataModel : BaseDataModel
    {
        public int total { get; set; }
        public List<Roadway> roadWay { get; set; }
    }

    public class Roadway
    {
        public string laneCode { get; set; }
        public string name { get; set; }
        public RoadType type { get; set; }
        public string releaseMode { get; set; }
        public string autoRelease { get; set; }
    }

    /// <summary>
    /// 车道类型
    /// </summary>
    public enum RoadType
    {
        入口 = 1,
        出口不收费 = 2,
        出口收费 = 3,
    }

    /// <summary>
    /// 放行模式
    /// </summary>
    public enum ReleaseMode
    {
        车卡一致 = 0,
        单进单出 = 1,
    }

    /// <summary>
    /// 放行规则
    /// </summary>
    public enum AutoRelease
    {
        固定车放行 = 1,
        临时车放行 = 3,
        无牌车放行 = 5,
    }
}
