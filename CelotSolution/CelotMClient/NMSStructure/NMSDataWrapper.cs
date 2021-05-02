using CelotMClient.Manager;
using CelotMClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.NMSStructure
{
    public class NMSDataWrapper
    {
        NMSAlertManager nmsAlertmanger = new NMSAlertManager();
        public List<NMSReportCommand> NMSReportCommandList { get; set; }

        public NMSReportCommand GetRecentAlertNMSReportCommand()
        {
            foreach (NMSReportCommand command in NMSReportCommandList)
            {
                if (command.DeviceAlertCassification != DeviceAlertCassification.UNKNOWN &&
                        command.DeviceAlertCassification != DeviceAlertCassification.NORMAL    && 
                        (
                            command.DeviceAlertCassification == DeviceAlertCassification.ABNORMAL ||
                            command.DeviceAlertCassification == DeviceAlertCassification.ALERT
                        )
                    )
                {
                    return command;
                }
            }
            return null;
        }

        public void CheckAlert(bool curStatusCheck)
        {
            nmsAlertmanger.init();
            nmsAlertmanger.CheckAlerts(NMSReportCommandList, curStatusCheck);
        }

        public List<NameValuePair> AllAlertsStats
        {
            get 
            {
                List<NameValuePair> alertStats = new List<NameValuePair>();
                NameValuePair pair;
                if (RouterErrCnt > 0)
                {
                    pair = new NameValuePair { Name = AlertType.Router.Parse(), Value = RouterErrCnt };
                    alertStats.Add(pair);
                }

                if (UnknownDeviceErrCnt> 0)
                {
                    pair = new NameValuePair { Name = AlertType.UnknownDevice.Parse(), Value = UnknownDeviceErrCnt};
                    alertStats.Add(pair);
                }

                if (Lan1ErrCnt > 0)
                {
                    pair = new NameValuePair { Name = AlertType.Lan1.Parse(), Value = Lan1ErrCnt};
                    alertStats.Add(pair);
                }

                if (Lan2ErrCnt > 0)
                {
                    pair = new NameValuePair { Name = AlertType.Lan2.Parse(), Value = Lan2ErrCnt};
                    alertStats.Add(pair);
                }

                if (NetworkErrCnt > 0)
                {
                    pair = new NameValuePair { Name = AlertType.Network.Parse(), Value = NetworkErrCnt };
                    alertStats.Add(pair);
                }

                if (ExtPowerErrCnt > 0)
                {
                    pair = new NameValuePair { Name = AlertType.ExtPower.Parse(), Value = ExtPowerErrCnt };
                    alertStats.Add(pair);
                }

                if (RssiLeveErrCnt> 0)
                {
                    pair = new NameValuePair { Name = AlertType.RssiLevel.Parse(), Value = RssiLeveErrCnt };
                    alertStats.Add(pair);
                }

                if (LowBatteryErrCnt > 0)
                {
                    pair = new NameValuePair { Name = AlertType.LowBattery.Parse(), Value = LowBatteryErrCnt };
                    alertStats.Add(pair);
                }

                if (DeviceIdErrCnt > 0)
                {
                    pair = new NameValuePair { Name = AlertType.DeviceIdError.Parse(), Value = DeviceIdErrCnt };
                    alertStats.Add(pair);
                }
                return alertStats;
            }
        }

        public List<NameValuePair> TechStats
        {
            get
            {
                List<NMSReportCommand> nmsReportList = new List<NMSReportCommand>();
                foreach (NMSReportCommand nmsReportCommand in NMSReportCommandList)
                {
                    if (nmsReportCommand.nms_reprot_t != null)
                    {
                        nmsReportList.Add(nmsReportCommand);
                    }
                }

                List<NameValuePair> nameValuePairList = 
                    (from report in nmsReportList
                     group report by report.nms_reprot_t.data.moduleband into g
                     select new NameValuePair
                     {
                        Name = ((ModuleBand)g.First().nms_reprot_t.data.moduleband).Parse(),
                        Value = (int)g.Count()
                      }).ToList();
                return nameValuePairList;
            }
        }

        public List<NameValuePair> RssiLevelStats
        {
            get
            {
                List<NMSReportCommand> nmsReportList = new List<NMSReportCommand>();
                foreach (NMSReportCommand nmsReportCommand in NMSReportCommandList)
                {
                    if (nmsReportCommand.nms_reprot_t != null)
                    {
                        nmsReportList.Add(nmsReportCommand);
                    }
                }
                List<NameValuePair> nameValuePairList = (from rssi in
                    (from r in nmsReportList
                     select
                        //nmsAlertmanger.getRssiLevel(r.nms_reprot_t.data.modulesignal)
                         r.nms_reprot_t.data.modulesignal <= -105 ? 1 :
                         r.nms_reprot_t.data.modulesignal >= -105 && r.nms_reprot_t.data.modulesignal <= -95 ? 2 :
                         r.nms_reprot_t.data.modulesignal >= -94 && r.nms_reprot_t.data.modulesignal <= -90 ? 3 :
                         r.nms_reprot_t.data.modulesignal >= -89 ? 4 : 0
                     )
                    group rssi by rssi into g
                    select new NameValuePair
                    {
                        Name = ((RssiLevel)g.First()).Parse(),
                        Value = (int)g.Count()
                    }).ToList();

                return nameValuePairList;
            }
        }

        public int RouterErrCnt { 
            get 
            {
                return nmsAlertmanger.RouterErrCnt;
            } 
        }
        public int UnknownDeviceErrCnt
        {
            get
            {
                return nmsAlertmanger.UnknownDeviceErrCnt;
            }
        }
        public int Lan1ErrCnt
        {
            get
            {
                return nmsAlertmanger.Lan1ErrCnt;
            }
        }
        public int Lan2ErrCnt
        {
            get
            {
                return nmsAlertmanger.Lan2ErrCnt;
            }
        }
        public int NetworkErrCnt
        {
            get
            {
                return nmsAlertmanger.NetworkErrCnt;
            }
        }
        public int ExtPowerErrCnt
        {
            get
            {
                return nmsAlertmanger.ExtPowerErrCnt;
            }
        }
        public int RssiLeveErrCnt
        {
            get
            {
                return nmsAlertmanger.RssiLeveErrCnt;
            }
        }
        public int LowBatteryErrCnt
        {
            get
            {
                return nmsAlertmanger.LowBatteryErrCnt;
            }
        }
        public int DeviceIdErrCnt
        {
            get
            {
                return nmsAlertmanger.DeviceIdErrCnt;
            }
        }

        public NMSReportCommand Find(int serialNo)
        {
            NMSReportCommand nmsReportCommand =
                this.NMSReportCommandList.Find(
                    r =>
                    {
                        if (r.Device != null)
                        {
                            return r.Device.SerialNo == serialNo;
                        }
                        return false;
                    }
                   );
            return nmsReportCommand;
        }
       
    }
}
