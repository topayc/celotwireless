using CelotMClient.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.Model.NMS
{
    public class DeviceReportCommand
    {
        private List<AlertClass> alertClassList = new List<AlertClass>();
        public void AddAlert(AlertClass alertClass)
        {
            this.alertClassList.Add(alertClass);
        }
        public List<AlertClass> GetAlertList()
        {
            return this.alertClassList;
        }

        public String GetAlertAllMessages()
        {
            string alertMessages = null;
            foreach (AlertClass alertClass in alertClassList)
            {
                alertMessages += "," + alertClass.ErrorMessage;
            }
            return alertMessages;
        }

        public bool IsHaveAlert(AlertType alert)
        {   bool isHave = false;
            foreach(AlertClass alertClass in alertClassList){
                if (alertClass.AlertType == alert)
                {
                    isHave = true;
                    break;
                }
            }
            return isHave;
        }
        public bool isProblem()
        {
            return this.alertClassList.Count > 0 ? true : false;
        }
        //Device Group Property
        public string GroupName { get; set; }
        public string GetGroupNameString()
        {
            return this.GetStringFromString(this.GroupName);

        }

        //Deviice Property 
        public int DeviceNo { get; set; }
        public string GetDeviceNoString()
        {
            return this.GetStringFromInt(this.DeviceNo);
        }
     
        public string Name { get; set; }
        public string GetNameString()
        {
            return this.GetStringFromString(this.Name);
        }

        public int SerialNo { get; set; }
        public string GetSerialNoString()
        {
            return this.GetStringFromInt(this.SerialNo);
        }
        public int PhoneNumber { get; set; }
        public string GetPhoneNumberString()
        {
            return this.GetStringFromInt(this.PhoneNumber);
        }
        public string RouterIp { get; set; }
        public string GetRouterIpString()
        {
            return this.GetStringFromString(this.RouterIp);
        }
   
        public float Latitude { get; set; }
        public string GetLatitudeString()
        {
            if (this.Latitude < 1)
            {
                return "";
            }
            else
            {
                return this.Latitude.ToString();
            }
        }
        
        public float Longitude { get; set; }
        public string GetLongitudeString()
        {
            if (this.Longitude < 1)
            {
                return "";
            }
            else
            {
                return this.Longitude.ToString();
            }
        }

        public string Des { get; set; }
        public string GetDesString()
        {
            return this.GetStringFromString(this.Des);
        }
        public int SmsSupport { get; set; }
        public string GetSmsSupportString()
        {
            if (!this.IsDeviceInfoExisted())
            {
                return "";
            }
            else
            {
                return ((SmsSupport)this.SmsSupport).Parse();
            }
        }
        public int BatterySupport { get; set; }
        public string GetBatterySupportString()
        {
            if (!this.IsDeviceInfoExisted())
            {
                return "";
            }
            else
            {
                return ((BatterySupport)this.BatterySupport).Parse();
            }
        }

        public int WifiSupport { get; set; }
        public string GetWifiSupportString()
        {
            if (!this.IsDeviceInfoExisted())
            {
                return "";
            }
            else
            {
                return ((BatterySupport)this.WifiSupport).Parse();
            }
        }

        public int VpnSupport { get; set; }
        public string GetVpnSupportString()
        {
            if (!this.IsDeviceInfoExisted())
            {
                return "";
            }
            else
            {
                return ((VpnSupport)this.VpnSupport).Parse();
            }
        }
       // public int DeviceRegDate { get; set; }

        //Device Report Property 
        public int DeviceReportNo { get; set; }
        public string GetDeviceReportNoString()
        {
            return this.GetStringFromInt(this.DeviceReportNo);
        }
        public int RefDeviceNo { get; set; }
        public string GetRefDeviceNoString()
        {
            return this.GetStringFromInt(this.RefDeviceNo);
        }

        public int SessionId { get; set; }
        public string GetSessionIdString()
        {
            return this.GetStringFromInt(this.SessionId);
        }


        public int MessageType { get; set; }
        public String GetMessageTypeString()
        {
            if (!this.IsReportExisted())
            {
                return "";
            }
            else
            {
                return ((MessageType)this.MessageType).Parse();
            }
            
        }
        public int ProVer { get; set; }
        public string GetProVerString()
        {
            if (IsReportExisted())
            {
                return this.GetStringFromInt(this.ProVer);
            }
            else
            {
                return "";
            }
        }
        public int Ethernet1State { get; set; }
        public string GetEthernet1StateString()
        {
            if (!this.IsReportExisted())
            {
                return "";
            }
            else
            {
                return ((EthernetStatus)this.Ethernet1State).Parse();
            }
        } 
        public int Ethernet2State { get; set; }
        public string GetEthernet2StateString()
        {
            if (!this.IsReportExisted())
            {
                return "";
            }
            else
            {
                return ((EthernetStatus)this.Ethernet2State).Parse();
            }
        } 

        public int NetworkState { get; set; }
        public string GetNetworkStateString()
        {
            if (!this.IsReportExisted())
            {
                return "";
            }
            else
            {
                return ((NetworkState)this.NetworkState).Parse();
            }
        } 

        public int ExternalPower { get; set; }
        public string GetExternalPowerString()
        {
            if (!this.IsReportExisted())
            {
                return "";
            }
            else
            {
                return ((ExternalPower)this.ExternalPower).Parse();
            }
        } 
        public int UseRxAmount { get; set; }
        public string GetUseRxAmount()
        {
            return this.GetStringFromInt(this.UseRxAmount);
        }
        public int UseTxAmount { get; set; }
        public string GetUsetxAmount()
        {
            return this.GetStringFromInt(this.UseTxAmount);
        }

        public string CurrentIpAddress { get; set;}
        public string GetCurrentIpAddressString()
        {
            return this.GetStringFromString(this.CurrentIpAddress);
        }

        public String GetLanIPString()
        {
            if (!this.IsReportExisted())
            {
                return "";
            }
            else
            {
                int index = this.CurrentIpAddress.IndexOf("(");
                if (index < 0)
                {
                    return "";
                }
                int index2 = this.CurrentIpAddress.LastIndexOf(")");
                return this.CurrentIpAddress.Substring(index+1, index2-index-1);
            }
        
        }

        public String GetWanIPString()
        {

            if (!this.IsReportExisted())
            {
                return "";
            }
            else
            {
                int index = this.CurrentIpAddress.IndexOf("(");
                if (index < 0)
                {
                    return "";
                }
                return this.CurrentIpAddress.Substring(0, index);
            }
           
        }
        public string CurrentTime { get; set; }
        public string GetCurrentTimeString()
        {
            return this.GetStringFromString(this.CurrentTime );
        }
        public int ModuleBand { get; set; }
        public string GetModuleBandString()
        {
            if (!this.IsReportExisted())
            {
                return "";
            }
            else
            {
                return ((ModuleBand)this.ModuleBand).Parse();
            }
        } 
        public int ModuleService { get; set; }
        public string GetModuleServiceString()
        {
            if (!this.IsReportExisted())
            {
                return "";
            }
            else
            {
                return ((ModuleService)this.ModuleService).Parse();
            }
        } 

        public int ModuleSignal { get; set; }
        public string GetModuleSignalString()
        {
            return this.GetStringFromInt(this.ModuleSignal);
        }
        public int DeviceStatus{ get; set; }
        public string GetDeviceStatusString()
        {
            if (!this.IsReportExisted())
            {
                return "";
            }
            else
            {
                return ((DeviceStatus)this.DeviceStatus).Parse();
            }
        } 

        public int WifiStatus { get; set; }
        public string GetWifiStatusString()
        {
            if (!this.IsReportExisted())
            {
                return "";
            }
            else
            {
                return ((WifiStatus)this.WifiStatus).Parse();
            }
        } 

        public int VpnStatus { get; set; }
        public string GetVpnStatusString()
        {
            if (!this.IsReportExisted())
            {
                return "";
            }
            else
            {
                return ((VpnStatus)this.VpnStatus).Parse();
            }
        } 

        public int NewSms { get; set; }
        public string GetNewSmsString()
        {
            return this.GetStringFromInt(this.NewSms);
        }
        public string SwVersion { get; set; }
        public string GetSwVersionString()
        {
            return this.GetStringFromString(this.SwVersion);
        }

        public int RptTime { get; set; }
        public string GetRptTimeString()
        {
            return this.GetStringFromInt(this.RptTime);
        }

        public int RsrqSignal { get; set; }
        public string GetRsrqSignalString()
        {
            return this.GetStringFromInt(this.RsrqSignal);
        }

        public int RsrpSignal { get; set; }
        public string GetRsrpSignalString()
        {
            return this.GetStringFromInt(this.RsrpSignal);
        }

        public string HwVersion { get; set; }
        public string GetHwVersionString()
        {
            return this.GetStringFromString(this. HwVersion);
        }
        public int RptPort { get; set; }
        public string GetRptPortString()
        {
            return this.GetStringFromInt(this. RptPort);
        }

        public int RmtPort { get; set; }
        public string GetRmtPortString()
        {
            return this.GetStringFromInt(this.RmtPort);
        }

        public string ExtDevice1 { get; set; }
        public int GetBatteryValue()
        {
            if (!String.IsNullOrEmpty(this.ExtDevice1))
            {
                string value = this.ExtDevice1.Split(',')[0];
                return Convert.ToInt32(value);
            }
            else
            {
                return 0;
            }
            
        }
        public string ExtDevice2 { get; set; }
        // public int DeviceReportRegDate { get; set; }
        public int RssiLevel { get; set; }

        public int ResetType { get; set; }
        public string ResetTime { get; set; }
        
        private string GetStringFromInt(int no)
        {
            if (no < 1)
            {
                return "";
            }
            else
            {
                return no.ToString();
            }
        }

        private string GetStringFromString(String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return "";
            }
            else
            {
                return str;
            }
        }

        public bool IsDeviceInfoExisted(){
            return this.PhoneNumber != 0 ? true : false;
        }

        public bool IsReportExisted()
        {
            return this.SessionId != 0 ? true : false;
        }

      
    }
}
