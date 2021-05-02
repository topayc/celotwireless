using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.Manager
{
    public class Constants
    {
        public static int CREATE = 1;
        public static int MODIFY = 2;

        public static int DATETYPE_DAY = 1;
        public static int DATETYPE_MONTH = 2;
    }

    public class MenuIndex
    {
        public static int Dashboard = 0;
        public static int Device = 1;
        public static int Alert = 2;
        public static int Application = 3;
        public static int Sms = 4;

    }
    public class ExcelColumnIndex{
        public static int NAME = 1;
        public static int SERIAL_NUMBER = 2;
        public static int SECURE_CODE = 3;
        public static int PHONE_NUMBER = 4;
        public static int GROUP = 5;
        public static int ROUTER_IP = 6;
        public static int LATITUDE = 7;
        public static int LONGITUDE = 8;
        public static int SMS = 9;
        public static int BATTERY = 10;
        public static int WIFI = 11;
        public static int VPN = 12;
        public static int DES = 13;
    }

    public enum SmsSupport
    {
        SUPPORTED = 0,
        UNSUPPORTED = 1
    }

    public enum BatterySupport
    {
        SUPPORTED = 0,
        UNSUPPORTED = 1
    }

    public enum WifiSupport
    {
        SUPPORTED = 0,
        UNSUPPORTED = 1
    }

    public enum VpnSupport
    {
        SUPPORTED = 0,
        UNSUPPORTED = 1
    }

    public enum MessageType
    {
        REPORT = 0,
        EVENT = 1
    }

    public enum EthernetStatus
    {
        NORMAL = 0,
        ABNORMAL = 1
    }
    public enum NetworkState
    {
        WIRE = 0,
        WIRELESS = 1
    }

    public enum ExternalPower
    {
        NORMAL = 0,
        ABNORMAL = 1
    }

    public enum ModuleBand
    {
        MODEM_BAND_NONE = 0,
        MODEM_BNAD_CDMA = 1,
        MODEM_BAND_WCDMA = 2,
        MODEM_BNAD_LTE = 3,
    }

    public enum ModuleService
    {
        DSAT_NET_REG_NONE = 0,           /* Not registered, not searching */
        DSAT_NET_REG_HOME = 1,           /* Registered on home network */
        DSAT_NET_REG_SEARCHING = 2,      /* Not registered, searching */
        DSAT_NET_REG_DENIED = 3,         /* Registration denied */
        DSAT_NET_REG_UNKNOWN = 4,        /* Unknown registration state */
        DSAT_NET_REG_ROAMING = 5,        /* Registered on roaming network */
        DSAT_REG_REGISTERED_MAX          /* Internal use only! */
    }
    public enum RssiLevel
    {
        LEVEL_0,  // No service 
        LEVEL_1,  // - 105 이하  
        LEVEL_2,  // -95 ~ 104 
        LEVEL_3,  // -90 ~ 94
        LEVEL_4,  // -89 이상 
    }

    public enum DeviceStatus
    {
        NORMAL = 0,
        ABNORMAL = 1,
    }

    public enum WifiStatus
    {
        OFF = 0,
        ON = 1,
    }

    public enum VpnStatus
    {
        NOT_USE = 0,
        USE = 1
    }

    public enum SmsStatus
    {
        NORMAL = 0,
        ERROR = 1
    }
  

    public static class ProtocolEnumExtension
    {
        public static String Parse(this SmsSupport support)
        {
            String str = "";
            switch (support)
            {
                case SmsSupport.SUPPORTED:
                    str = "ON";
                    break;
                case SmsSupport.UNSUPPORTED:
                    str = "OFF";
                    break;
                default:
                    str = "N/A";
                    break;
            }
            return str;
        }

        public static String ToString(this SmsSupport support)
        {
            String str = "";
            switch (support)
            {
                case SmsSupport.SUPPORTED:
                    str = "ON";
                    break;
                case SmsSupport.UNSUPPORTED:
                    str = "OFF";
                    break;
                default:
                    str = "N/A";
                    break;
            }
            return str;
        }

        public static String Parse(this BatterySupport support)
        {
            String str = "";
            switch (support)
            {
                case BatterySupport.SUPPORTED:
                    str = "ON";
                    break;
                case BatterySupport.UNSUPPORTED:
                    str = "OFF";
                    break;
                default:
                    str = "N/A";
                    break;
            }
            return str;
        }

        public static String Parse(this WifiSupport support)
        {
            String str = "";
            switch (support)
            {
                case WifiSupport.SUPPORTED:
                    str = "ON";
                    break;
                case WifiSupport.UNSUPPORTED:
                    str = "OFF";
                    break;
                default:
                    str = "N/A";
                    break;
            }
            return str;
        }

        public static String Parse(this VpnSupport support)
        {
            String str = "";
            switch (support)
            {
                case VpnSupport.SUPPORTED:
                    str = "ON";
                    break;
                case VpnSupport.UNSUPPORTED:
                    str = "OFF";
                    break;
                default:
                    str = "N/A";
                    break;
            }
            return str;
        }

        public static String Parse(this MessageType support)
        {
            String str = "";
            switch (support)
            {
                case MessageType.EVENT:
                    str = "EVENT";
                    break;
                case MessageType.REPORT:
                    str = "REPORT";
                    break;
                default:
                    str = "N/A";
                    break;
            }
            return str;
        }

        public static String Parse(this RssiLevel level)
        {
            String str = "";
            switch (level)
            {
                case RssiLevel.LEVEL_0:
                    str = "LEVEL 0";
                    break;
                case RssiLevel.LEVEL_1:
                     str = "LEVEL 1";
                    break;
                case RssiLevel.LEVEL_2:
                     str = "LEVEL 2";
                    break;
                case RssiLevel.LEVEL_3:
                    str = "LEVEL 3";
                    break;
                case RssiLevel.LEVEL_4:
                    str = "LEVEL 4";
                    break;
                default:
                    str = "N/A";
                    break;
            }
            return str;
        }



        public static Color ColorParse(this RssiLevel rssiLevel)
        {
            Color color = Color.Black;
            switch (rssiLevel)
            {
                case RssiLevel.LEVEL_0:
                    color = Color.Red;
                    break;
                case RssiLevel.LEVEL_1:
                    color = Color.Orange;
                    break;
                case RssiLevel.LEVEL_2:
                    color = Color.Blue;
                    break;
                case RssiLevel.LEVEL_3:
                    color = Color.Purple;
                    break;
                case RssiLevel.LEVEL_4:
                    color = Color.Green;
                    break;
            }
            return color;
        }

        public static String Parse(this EthernetStatus ethernetStauts)
        {
            String str = "";
            switch (ethernetStauts)
            {
                case EthernetStatus.NORMAL:
                    str = "NORMAL";
                    break;
               case EthernetStatus.ABNORMAL:
                    str = "ABNORMAL";
                    break;
                default :
                    str = "N/A";
                    break;
            }
            return str;
        }

        public static String Parse(this NetworkState state)
        {
            String str = "";
            switch (state)
            {
                case NetworkState.WIRE:
                    str = "WIRE";
                    break;
                case NetworkState.WIRELESS:
                    str = "WIRELESS";
                    break;
                default:
                    str = "N/A";
                    break;
            }
            return str;
        }

        public static String Parse(this SmsStatus state)
        {
            String str = "";
            switch (state)
            {
                case SmsStatus.NORMAL:
                    str = "ON";
                    break;
                case SmsStatus.ERROR:
                    str = "OFF";
                    break;
                default:
                    str = "N/A";
                    break;
            }
            return str;
        }

        public static String Parse(this ExternalPower state)
        {
            String str = "";
            switch (state)
            {
                case ExternalPower.NORMAL:
                    str = "ON";
                    break;
                case ExternalPower.ABNORMAL:
                    str = "OFF";
                    break;
                default:
                    str = "N/A";
                    break;
            }
            return str;
        }

        public static String Parse(this ModuleBand state)
        {
            String str = "";
            switch (state)
            {
                case ModuleBand.MODEM_BAND_NONE:
                    str = "NONE";
                    break;
                case ModuleBand.MODEM_BNAD_CDMA:
                    str = "CDMA";
                    break;
                case ModuleBand.MODEM_BAND_WCDMA:
                    str = "WCDMA";
                    break;
                case ModuleBand.MODEM_BNAD_LTE:
                    str = "LTE";
                    break;
            }
            return str;
        }

        public static String Parse(this ModuleService state)
        {
            String str = "";
            switch (state)
            {
                case ModuleService.DSAT_NET_REG_NONE:
                    str = "None";
                    break;
                case ModuleService.DSAT_NET_REG_HOME:
                    str = "Home";
                    break;
                case ModuleService.DSAT_NET_REG_SEARCHING:
                    str = "Searching";
                    break;
                case ModuleService.DSAT_NET_REG_DENIED:
                    str = "Denied";
                    break;
                case ModuleService.DSAT_NET_REG_UNKNOWN:
                    str = "Unknown";
                    break;
                case ModuleService.DSAT_NET_REG_ROAMING:
                    str = "Roaming";
                    break;
                case ModuleService.DSAT_REG_REGISTERED_MAX:
                    str = "Max";
                    break;
            }
            return str;
        }

        public static String Parse(this DeviceStatus state)
        {
            String str = "";
            switch (state)
            {
                case DeviceStatus.NORMAL:
                    str = "NORMAL";
                    break;
                case DeviceStatus.ABNORMAL:
                    str = "ABNORMAL";
                    break;
                default:
                    str = "N/A";
                    break;
            }
            return str;
        }

        public static String Parse(this WifiStatus state)
        {
            String str = "";
            switch (state)
            {
                case WifiStatus.OFF:
                    str = "OFF";
                    break;
                case WifiStatus.ON:
                    str = "ON";
                    break;
                default:
                    str = "N/A";
                    break;
            }
            return str;
        }

        public static String Parse(this VpnStatus state)
        {
            String str = "";
            switch (state)
            {
                case VpnStatus.NOT_USE:
                    str = "OFF";
                    break;
                case VpnStatus.USE:
                    str = "ON";
                    break;
                default:
                    str = "N/A";
                    break;
            }
            return str;
        }
    }
}
