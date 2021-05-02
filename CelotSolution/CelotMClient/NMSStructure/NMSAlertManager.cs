using CelotMClient.Model;
using CelotMClient.NMSStructure;
using CelotMClient.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.Manager
{
    public class NMSAlertManager
    {
        public int RouterErrCnt { get; set; }
        public int UnknownDeviceErrCnt { get; set; }
        public int Lan1ErrCnt { get; set; }
        public int Lan2ErrCnt { get; set; }
        public int NetworkErrCnt { get; set; }
        public int ExtPowerErrCnt { get; set; }
        public int RssiLeveErrCnt { get; set; }
        public int LowBatteryErrCnt { get; set; }
        public int DeviceIdErrCnt { get; set; }

        public void init()
        {
            RouterErrCnt = 0;
            UnknownDeviceErrCnt = 0;
            Lan1ErrCnt  = 0;
            Lan2ErrCnt  = 0;
            NetworkErrCnt  = 0;
            ExtPowerErrCnt  = 0;
            RssiLeveErrCnt  = 0;
            LowBatteryErrCnt  = 0;
            DeviceIdErrCnt = 0;
        }

        public  void CheckAlerts(List<NMSReportCommand> nmsReportCommandList, bool routerErrorCheck)
        {
            foreach (NMSReportCommand nmsReportCommand in nmsReportCommandList)
            {
                CheckAlerts(nmsReportCommand, routerErrorCheck);
                if (nmsReportCommand.AlertList.Count < 1)
                {
                    AlertClass alertClass = new AlertClass();
                    alertClass.AlertType = AlertType.No;
                    alertClass.AddErrorMessage("Normal");
                    nmsReportCommand.AddAlert(alertClass);
                    nmsReportCommand.DeviceAlertCassification = DeviceAlertCassification.NORMAL;
                    
                }
            }
        }

        
        public void CheckAlerts(NMSReportCommand nmsReportCommand, bool curStatusCheck)
        {
            //check route  error if a variable curStatusCheck is true
            if (curStatusCheck)
            {
                if (checkRouterError(nmsReportCommand)) return;
            }

            //Unknown Device  
            if (checkUnknownDeviceAlert(nmsReportCommand)) return;
            
            //Alerts check
                checkLan1Alert(nmsReportCommand);
                checkLan2Alert(nmsReportCommand);
                //네트워크 상태 , Wire 와 Wireless 중 어느상태가 에러인지 불문명하여 일단 주석
                //checkNetworkAlert(nmsReportCommand);
                checkExtPowerAlert(nmsReportCommand);
                checkRssiAlert(nmsReportCommand);
                checkLowBatteryAlert(nmsReportCommand);
                checkDeviceIdErrorAlert(nmsReportCommand);
        }

        public bool checkRouterError(NMSReportCommand nmsReportCommand)
        {
            if (nmsReportCommand.Device != null)
            {
                Boolean isRouterError = false;
                DateTime compareTime;
                if (nmsReportCommand.nms_reprot_t == null)
                {
                    compareTime = CelotUtility.UnixTimeStampToDateTime(nmsReportCommand.Device.DeviceRegDate);
                    int result = DateTime.Compare(DateTime.Now, compareTime.AddMinutes(ApplicationConfig.Instance().NmsReportLimit));
                    if (result > 0)
                    {
                        isRouterError = true;
                    }
                }
                else
                {
                    compareTime = DateTime.ParseExact(nmsReportCommand.nms_reprot_t.data.current_time, "yyMMddHHmmss", null);
                    int result = DateTime.Compare(DateTime.Now, compareTime.AddMinutes(ApplicationConfig.Instance().NmsReportLimit));
                    if (result > 0)
                    {
                        isRouterError = true;
                    }
                }

                if (isRouterError)
                {
                    AlertClass alertClass = new AlertClass();
                    alertClass.AlertType = AlertType.Router;
                    alertClass.AddErrorMessage("No NMS Report");
                    nmsReportCommand.DeviceAlertCassification = DeviceAlertCassification.ABNORMAL; 
                    nmsReportCommand.AddAlert(alertClass);
                    alertClass.AlertDuration = CelotUtility.getTimeAgo(compareTime);
                    RouterErrCnt++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool checkUnknownDeviceAlert(NMSReportCommand nmsReportCommand)
        {
            if (nmsReportCommand.Device == null && nmsReportCommand.nms_reprot_t != null)
            {
                AlertClass alertClass = new AlertClass();
                alertClass.AlertType = AlertType.UnknownDevice;
                nmsReportCommand.DeviceAlertCassification = DeviceAlertCassification.UNKNOWN; 
                alertClass.AddErrorMessage("Unknown Router");
                nmsReportCommand.AddAlert(alertClass);
                UnknownDeviceErrCnt++;
                return true;
            }
            return false;
        }

        public void checkLan1Alert(NMSReportCommand nmsReportCommand)
        {
            if (nmsReportCommand.nms_reprot_t == null) return;
            EthernetStatus eStatus = (EthernetStatus)nmsReportCommand.nms_reprot_t.data.ethernet1_state;
            if (eStatus == EthernetStatus.ABNORMAL)
            {
                AlertClass alertClass = new AlertClass();
                nmsReportCommand.DeviceAlertCassification = DeviceAlertCassification.ALERT; 
                alertClass.AlertType = AlertType.Lan1;
                alertClass.AddErrorMessage("Lan 1 Abnormal");
                nmsReportCommand.AddAlert(alertClass);
                Lan1ErrCnt++;
                
            }
        }

        public void checkLan2Alert(NMSReportCommand nmsReportCommand)
        {
            if (nmsReportCommand.nms_reprot_t == null) return;
            EthernetStatus eStatus = (EthernetStatus)nmsReportCommand.nms_reprot_t.data.ethernet2_state;
            if (eStatus == EthernetStatus.ABNORMAL)
            {
                AlertClass alertClass = new AlertClass();
                nmsReportCommand.DeviceAlertCassification = DeviceAlertCassification.ALERT; 
                alertClass.AlertType = AlertType.Lan2;
                alertClass.AddErrorMessage("Lan 2 Abnormal");
                Lan2ErrCnt++;
                nmsReportCommand.AddAlert(alertClass);
            }
        }

        //네트워크 상태가 Wire, Wireless 중 어떤 상태가 에러인지 불분명함
        public void checkNetworkAlert(NMSReportCommand nmsReportCommand)
        {
            if (nmsReportCommand.nms_reprot_t == null) return;
            NetworkState nState = (NetworkState)nmsReportCommand.nms_reprot_t.data.network_state;
            if (nState == NetworkState.WIRE)
            {
                AlertClass alertClass = new AlertClass();
                nmsReportCommand.DeviceAlertCassification = DeviceAlertCassification.ALERT; 
                alertClass.AlertType = AlertType.Network;
                alertClass.AddErrorMessage("Wire");
                NetworkErrCnt++;
                nmsReportCommand.AddAlert(alertClass);
            }

            if (nState == NetworkState.WIRELESS)
            {
                AlertClass alertClass = new AlertClass();
                alertClass.AlertType = AlertType.Network;
                nmsReportCommand.DeviceAlertCassification = DeviceAlertCassification.ALERT; 
                alertClass.AddErrorMessage("Wireless");
                NetworkErrCnt++;
                nmsReportCommand.AddAlert(alertClass);
            }
        }

        public void checkExtPowerAlert(NMSReportCommand nmsReportCommand)
        {
            if (nmsReportCommand.nms_reprot_t == null) return;
            ExternalPower state = (ExternalPower)nmsReportCommand.nms_reprot_t.data.external_power;
            if (state == ExternalPower.ABNORMAL)
            {
                AlertClass alertClass = new AlertClass();
                alertClass.AlertType = AlertType.ExtPower;
                nmsReportCommand.DeviceAlertCassification = DeviceAlertCassification.ALERT; 
                alertClass.AddErrorMessage("External Power Abnormal");
                ExtPowerErrCnt++;
                nmsReportCommand.AddAlert(alertClass);
            }
        }

        public void checkRssiAlert(NMSReportCommand nmsReportCommand)
        {
            if (nmsReportCommand.nms_reprot_t == null) return;
            int rssiLevel = getRssiLevel(nmsReportCommand.nms_reprot_t.data.modulesignal);
            if (rssiLevel <= (int)RssiLevel.LEVEL_1)
            {
                AlertClass alertClass = new AlertClass();
                alertClass.AlertType = AlertType.RssiLevel;
                nmsReportCommand.DeviceAlertCassification = DeviceAlertCassification.ALERT; 
                alertClass.AddErrorMessage("RSSI Level Error");
                RssiLeveErrCnt++;
                nmsReportCommand.AddAlert(alertClass);
            }
        }

        public int getRssiLevel(int moduleSignal)
        {
            int level = 0;
            if (moduleSignal <= -105) level = 1;
            if (moduleSignal >= -105 && moduleSignal <= -95) level = 2;
            if (moduleSignal >= -94 && moduleSignal <= -90) level = 3;
            if (moduleSignal >= -89) level = 4;
            return level;
        }

        public void checkLowBatteryAlert(NMSReportCommand nmsReportCommand)
        {
            if (nmsReportCommand.nms_reprot_t == null) return;
            int lowBatteryLimit = ApplicationConfig.Instance().LowBatteryLimit;
            int battery = nmsReportCommand.nms_reprot_t.data.ext_device1[0];
            if (battery <= lowBatteryLimit)
            {
                AlertClass alertClass = new AlertClass();
                alertClass.AlertType = AlertType.LowBattery;
                alertClass.AddErrorMessage("Low Battery");
                nmsReportCommand.DeviceAlertCassification = DeviceAlertCassification.ALERT; 
                alertClass.ErrorMessage = "Low Battery";
                LowBatteryErrCnt++;
                nmsReportCommand.AddAlert(alertClass);
            }
        }

        public void checkDeviceIdErrorAlert(NMSReportCommand nmsReportCommand)
        {
            if (nmsReportCommand.nms_reprot_t == null || nmsReportCommand.Device == null) return;
            AlertClass alertClass = new AlertClass();
            alertClass.AlertType = AlertType.DeviceIdError;
            nmsReportCommand.DeviceAlertCassification = DeviceAlertCassification.ALERT; 
            alertClass.ErrorMessage = "";

            //아이피 불일치 조사 
            if (!nmsReportCommand.Device.RouterIp.Equals(nmsReportCommand.GetLanIPString()))
            {
                alertClass.AddErrorMessage("Inconsistent IP");
            }

            //SMS 상태 불일지 조사
            if ((SmsSupport)nmsReportCommand.Device.SmsSupport == SmsSupport.SUPPORTED)
            {
                if (nmsReportCommand.nms_reprot_t.data.newsms < 0)
                {
                    alertClass.AddErrorMessage("Inconsistent SMS Status");
                }
            }
            else
            {
                if (nmsReportCommand.nms_reprot_t.data.newsms > 0)
                {
                    alertClass.AddErrorMessage("Inconsistent SMS Status");
                }
            }

            //와이파이 상태 불일치 조사
            if ((WifiSupport)nmsReportCommand.Device.WifiSupport== WifiSupport.SUPPORTED)
            {
                if (nmsReportCommand.nms_reprot_t.data.wifistatus != 1 && nmsReportCommand.nms_reprot_t.data.wifistatus != 0)
                {
                    alertClass.AddErrorMessage("Inconsistent Wifie Status");
                }
            }
            else
            {
                if (nmsReportCommand.nms_reprot_t.data.wifistatus == 1 || nmsReportCommand.nms_reprot_t.data.wifistatus == 0)
                {
                    alertClass.AddErrorMessage("Inconsistent Wifie Status");
                }
            }

            //VPN 상태 불일치 조사
            if ((VpnSupport)nmsReportCommand.Device.VpnSupport == VpnSupport.SUPPORTED)
            {
                if (nmsReportCommand.nms_reprot_t.data.vpnstatus != 1 && nmsReportCommand.nms_reprot_t.data.vpnstatus != 0)
                {
                    alertClass.AddErrorMessage("Inconsistent VPN Status");
                }
            }
            else
            {
                if (nmsReportCommand.nms_reprot_t.data.vpnstatus == 1 || nmsReportCommand.nms_reprot_t.data.vpnstatus == 0)
                {
                    alertClass.AddErrorMessage("Inconsistent Vpn Status");
                }
            }

            ////배터리 상태 불일치 조사
            //if ((BatterySupport)nmsReportCommand.Device.BatterySupport == BatterySupport.SUPPORTED)
            //{
            //    if (nmsReportCommand.nms_reprot_t.data.ext_device1[0] < 0)
            //    {
            //        alertClass.AddErrorMessage("Inconsistent Battery Status");
            //    }
            //}
            //else
            //{
            //    if (nmsReportCommand.nms_reprot_t.data.ext_device1[0] > 0)
            //    {
            //        alertClass.AddErrorMessage("Inconsistent Battery Status");
            //    }
            //}

            if (alertClass.ErrorMessage.Length > 0)
            {
                DeviceIdErrCnt++;
                nmsReportCommand.AddAlert(alertClass);
            }
        }
    }
}
