using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.Dto
{
    public class DeviceChartDto
    {
        public int RssiLevel { get; set; }
        
        public uint Tech { get; set; }
        public uint Tx { get; set; }
        public uint Rx { get; set; }
        public string Time { get; set; }
    }
}
