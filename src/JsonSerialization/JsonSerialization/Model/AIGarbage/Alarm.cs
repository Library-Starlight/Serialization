using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerialization
{
    public enum AlarmType
    {
        起火_冒烟 = 41000,
        打包垃圾 = 41001,
        垃圾桶满溢 = 41002,
        暴露垃圾 = 41003,
        施工废弃料 = 41004,
    }

    public class Alarm
    {
        public int autoWzd { get; set; }
        public object actTaskPass { get; set; }
        public object actSpyjbz { get; set; }
        public object actSpyjtp { get; set; }
        public object actSpyjwj { get; set; }
        public bool onlydealtask { get; set; }
        public object actTaskid { get; set; }
        public object actTaskstatus { get; set; }
        public string tablename { get; set; }
        public string id { get; set; }
        public string uuid { get; set; }
        public object bt { get; set; }
        public string hkid { get; set; }
        [JsonConverter(typeof(EnumJsonConvert))]
        public AlarmType originaltype { get; set; }
        public string originaltypestr { get; set; }
        public string intelligenttype { get; set; }
        public string intelligenttypestr { get; set; }
        public string violationareas { get; set; }
        public int confidence { get; set; }
        public string audited { get; set; }
        public string collecttime { get; set; }
        public string collecttimestr { get; set; }
        public string finishedtime { get; set; }
        public int? durationtime { get; set; }
        public string durationtimestr { get; set; }
        public string auditeduser { get; set; }
        public object comment { get; set; }
        public string picurl { get; set; }
        public string thumbnailurl { get; set; }
        public string orgid { get; set; }
        public string orgname { get; set; }
        public string monitorcode { get; set; }
        public string monitorname { get; set; }
        public string eventcode { get; set; }
        public object auditresult { get; set; }
        public int? imgwidth { get; set; }
        public int? imgheight { get; set; }
        public object garbagepointname { get; set; }
        public string latesttime { get; set; }
        public string latesttimestr { get; set; }
        public object facepicurl { get; set; }
        public string clzt { get; set; }
        public object bz { get; set; }
        public object cjrxm { get; set; }
        public string gxrxm { get; set; }
        public object cjr { get; set; }
        public string cjsj { get; set; }
        public string gxr { get; set; }
        public string gxsj { get; set; }
        public object cjrzwxm { get; set; }
        public string gxrzwxm { get; set; }
        public string sfsc { get; set; }
    }
}
