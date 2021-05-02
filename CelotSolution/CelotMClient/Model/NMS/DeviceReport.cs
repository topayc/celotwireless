using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.Model.NMS
{
    public class DeviceReport
    {
        public int DeviceReportNo { get; set; }
        public int RefDeviceNo { get;set;}
        public int SessionId { get;set;}
        public int MessageType  { get;set;}
        public int ProVer { get; set; }
        public int Ethernet1State { get; set; }
        public int Ethernet2State { get; set; }
        public int NetworkState { get; set; }
        public int ExternalPower { get; set; }
        public int UseRxAmount { get; set; }
        public int UseTxAmount { get; set; }
        public string CurrentIpAddress{ get; set; }
        public string CurrentTime { get; set; }
        public int ModuleBand { get; set; }
        public int ModuleService { get; set; }
        public int ModuleSignal { get; set; }
        public int DeviceStatus { get; set; }
        public int WifiStatus { get; set; }
        public int VpnStatus { get; set; }
        public int NewSms { get; set; }
        public string SwVersion{ get; set; }
        public int RptTime { get; set; }
        public int RsrqSignal { get; set; }
        public int RsrpSignal { get; set; }
        public string HwVersion{ get; set; }
        public int RptPort { get; set; }
        public int RmtPort { get; set; }
        public string ExtDevice1 { get; set; }
        public string ExtDevice2 { get; set; }
        public int DeviceReportRegDate { get; set; }

        
        public int RssiLevel { get; set; }
  
    }
}
