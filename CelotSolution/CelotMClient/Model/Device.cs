using CelotMClient.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.Model
{
    public class Device 
    {
        public int DeviceNo { get; set; }
        public string GroupName{ get; set; }
        public string Name { get; set; }
        public int SerialNo { get; set; }
        public string SecuCode { get; set; }
        public int PhoneNumber { get; set; }
        public string RouterIp { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Des { get; set; }
        public int SmsSupport{ get;set; }
        public int BatterySupport{get;set;}
        public int WifiSupport{get;set;}
        public int VpnSupport{get; set;}

        public int ResetTime { get; set; }
        public int AlertStatus { get; set; }
        public string AlertOccurentTime { get; set; }
        public int DeviceRegDate { get; set; }

        
    }
}
