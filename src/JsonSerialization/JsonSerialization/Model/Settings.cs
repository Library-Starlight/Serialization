using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerialization.Model
{
    public class Settings
    {
        public Options Options { get; set; }
    }

    public enum Options
    { 
        Value0 = 0,
        Value1 = 1,
        Value2 = 2,
        Value3 = 3,
    }
}
