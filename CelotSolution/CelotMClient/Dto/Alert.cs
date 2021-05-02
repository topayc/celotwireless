using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.Model
{
    public static class EnumExtension
    {
        public static string Parse(this AlertType alertType)
        {
            switch (alertType)
            {
                case AlertType.Router:
                    return "Router";
                case AlertType.Lan1:
                    return "LAN1";
                case AlertType.Lan2:
                    return "LAN2";
                case AlertType.Network:
                    return "Network";
                case AlertType.ExtPower:
                    return "External Power";
                case AlertType.RssiLevel:
                    return "RSSI Level";
                case AlertType.LowBattery:
                    return "Low Battery";
                case AlertType.DeviceIdError:
                    return "Device Id Error";
                case AlertType.UnknownDevice:
                    return "Unknown Device";
                default:
                    throw new Exception();
            }
        }

        public static string Parse(this DeviceAlertCassification classfication)
        {
            switch (classfication)
            {
                case DeviceAlertCassification.NORMAL:
                    return "Normal ";
                case DeviceAlertCassification.ABNORMAL:
                    return "Abnormal (no nms)";
                case DeviceAlertCassification.ALERT:
                    return "Alert";
                case DeviceAlertCassification.UNKNOWN:
                    return "Unknow Router";
                default:
                    throw new Exception();
            }
        }

        public static string ParseShort(this DeviceAlertCassification classfication)
        {
            switch (classfication)
            {
                case DeviceAlertCassification.NORMAL:
                    return "normal";
                case DeviceAlertCassification.ABNORMAL:
                    return "router";
                case DeviceAlertCassification.ALERT:
                    return "alert";
                case DeviceAlertCassification.UNKNOWN:
                    return "unknown";
                default:
                    throw new Exception();
            }
        }

        public static string Parse(this ReportMode alertMode)
        {
            switch (alertMode)
            {
                case ReportMode.REPORT:
                    return "REPORT";
                case ReportMode.EVENT:
                    return "EVENT";
                default:
                    throw new Exception();
            }
        }
    }


    public enum AlertType
    {
        No,
        Router,  
        Lan1,
        Lan2,
        Network,
        ExtPower,
        RssiLevel,
        LowBattery,
        DeviceIdError,
        UnknownDevice

    }

    public enum ReportMode
    {
        REPORT,
        EVENT
    }

    public enum DeviceAlertCassification
    {
        NORMAL,  //0
        ABNORMAL, //1
        ALERT, //2
        UNKNOWN
    }

    public class AlertClass
    {
        public AlertType AlertType
        {
            get;
            set;
        }


        public String AlertTypeName
        {
            get { return this.AlertType.Parse();}
            set { }
        }
        public string ErrorMessage
        {
            get;
            set;
        }

        public void AddErrorMessage(String message)
        {
            this.ErrorMessage += " " + message;
        }

        public String AlertDuration { get; set; }
    }

  
}
