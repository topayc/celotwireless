using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.Manager
{
    public class ApplicationConfig
    {
        #region default const
        private static ApplicationConfig config;
        public static float DEFAULT_LATITUDE = 37.4969117f;
        public static float DEFAULT_LONGITUDE = 127.03292f;

        public static string DEFAULT_LOG_FILENAME = "celot.log";
        #endregion

        #region config file session and key const
        public static string SECTION_SERVICE = "SERVICE";
        public static string KEY_SERVICE_NAME = "SERVICE_NAME ";
        public static string KEY_SERVICE_DISPLAY_NAME = "SERVICE_DISPLAY_NAME";
        public static string KEY_SERVICE_FILE_PATH = "SERVICE_FILE_PATH";
        public static string KEY_SERVICE_PORT = "SERVICE_PORT";
        public static string KEY_MAX_SESSION_COUNT = "MAX_SESSION_COUNT ";

        public static string SECTION_CLIENT = "CLIENT";
        public static string KEY_REMOTE_PORT = "REMOTE_PORT";
        public static string KEY_CONCURRENT_DOWNLOAD_COUNT = "CONCURRENT_DOWNLOAD_COUNT";
        public static string KEY_LOW_BATTERY_LIMIT = "LOW_BATTERY_LIMIT";
        public static string KEY_NMS_REPORT_LIMIT = "NMS_REPOST_LIMIT";
        public static string KEY_UPDATE_PERIOD = "UPDATE_PERIOD";
        public static string KEY_LATITUDE = "LATITUDE";
        public static string KEY_LONGITUDE = "LONGITUDE";
        public static string KEY_PROTOCOL_DES = "PROTOCOL_DES";
        public static string KEY_LOG_FILENAME = "LOG_FILENAME";
        public static string KEY_INSTALL = "INSTALL";
        public static string KEY_MENU_SETTING = "MENU_SETTING";
        public static string KEY_DEVICE_COLUMN_SETTING = "DEVICE_COLUMN_SETTING";
        public static string KEY_ALERT_COLUMN_SETTING = "ALERT_COLUMN_SETTING";
        public static string KEY_FIRMWARE_DOWNLOAD_INFO = "FIRMWARE_DOWNLOAD_INFO";

        public static string SECTION_DATABASE = "DATABASE ";
        public static string KEY_DATABASE_HOST = "DATABASE_HOST";
        public static string KEY_DATABASE_NAME = "DATABASE_NAME";
        public static string KEY_DATABASE_ID = "DATABASE_ID";
        public static string KEY_DATABASE_PASSWORD = "DATABASE_PASSWORD";
        public static string KEY_DATABASE_CHARSET = "DATABASE_CHARSET";

        public static string SECTION_PROTOCOL = "PROTOCOL ";
        public static string KEY_USING__PROTOCOL = "USING_PROTOCOL";
        public static string KEY_NMS_SUPPORT = "NMS_SURPPORT";
        public static string KEY_SNMP_SUPPORT = "SNMP_SURPPORT";
        #endregion

        #region memeber variable
        private string _logFilename;
        private int _updatePeriod;
        private int _lowBatteryLimit;
        private int _nmsReportLimit;
        private int _concurrentDownloadCount;
        private int _remortPort;
        private int _servicePort;

        private double _latitude;
        private double _longitude;
        private string _protocolDes;
        private string _databaseHost;
        private string _databaseName;
        private string _databaseId;
        private string _databasePassword;
        private string _databaseCharset;
        private int _install;

        private int _usingProtocol;
        private bool _nmsSupport;
        private bool _snmpSupport;

        private string _serviceName;
        private string _serviceDisplayName;
        private string _serviceFilePath;

        private int _maxSessionCount;
        private bool initialized;

        private bool[] _menuSetting;
        private Dictionary<string, int> _deviceColumnSetting;
        private Dictionary<string, int> _alertColumnSetting;
        private Dictionary<string, string> _fiemwareDownloadInfo;

        #endregion

        private ApplicationConfig()
        {
        }
        public static ApplicationConfig Instance()
        {
            if (config == null)
            {
                config = new ApplicationConfig();
            }
            return config;
        }

        public void init()
        {
            Load();
        }

        public void Reload()
        {
            this.Load();
        }
        public void Load()
        {
            initialized = false;
            try
            {
                _remortPort = RemotePort;
                _updatePeriod = UpdatePeriod;
                _lowBatteryLimit = LowBatteryLimit;
                _nmsReportLimit = NmsReportLimit;
                _concurrentDownloadCount = ConcurrentDownloadCount;
                _latitude = Latitude;
                _longitude = Longitude;
                _protocolDes = ProtocolDes;
                _databaseHost = DatabaseHost;
                _databaseName = DatabaseName;
                _databaseId = DatabaseId;
                _databasePassword = DatabasePassword;
                _databaseCharset = DatabaseCharset;
                _logFilename = LogFilename;
                _usingProtocol = UsingProtocol;
                _nmsSupport = NMSSupport;
                _snmpSupport = SNMPSupport;
                _serviceName = ServiceName;
                _serviceDisplayName = ServiceDisplayName;
                _serviceFilePath = ServiceFilePath;
                _servicePort = ServicePort;
                _maxSessionCount = MaxSessionCount;
                _menuSetting = MenuSettings;
                _deviceColumnSetting = DeviceColumnSetting;
                _alertColumnSetting = AlertColumnSetting;
                _fiemwareDownloadInfo = FirmwareDownloadInfo;
                initialized = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        #region property

        public string LogFilename
        {
            get
            {
                if (initialized)
                {
                    return _logFilename;
                }
                else
                {
                    _logFilename = "".Equals(IniUtil.Instance().GetIniStringValue(SECTION_CLIENT, KEY_LOG_FILENAME)) ?
                        DEFAULT_LOG_FILENAME : IniUtil.Instance().GetIniStringValue(SECTION_CLIENT, KEY_LOG_FILENAME);
                    return _logFilename;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_LOG_FILENAME, value);
                this.initialized = false;
                Load();
            }
        }

        public string ServiceName
        {
            get
            {
                if (initialized)
                {
                    return _serviceName;
                }
                else
                {
                    _serviceName = IniUtil.Instance().GetIniStringValue(SECTION_SERVICE, KEY_SERVICE_NAME);
                    return _serviceName;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_SERVICE, KEY_SERVICE_NAME, value);
                this.initialized = false;
                Load();
            }
        }

        public bool[] MenuSettings
        {
            get
            {
                if (initialized)
                {
                    return _menuSetting;
                }
                else
                {
                    this._menuSetting = new bool[5];
                    string menuStr = IniUtil.Instance().GetIniStringValue(SECTION_CLIENT, KEY_MENU_SETTING);
                    if (String.IsNullOrEmpty(menuStr))
                    {
                        this._menuSetting[MenuIndex.Alert] = true;
                        this._menuSetting[MenuIndex.Application] = true;
                        this._menuSetting[MenuIndex.Dashboard] = true;
                        this._menuSetting[MenuIndex.Device] = true;
                        this._menuSetting[MenuIndex.Sms] = true;
                        return _menuSetting;
                    }
                    string[] menuSettingStr = menuStr.Split(',');

                    this._menuSetting[MenuIndex.Alert] = Convert.ToInt16(menuSettingStr[MenuIndex.Alert]) == 1 ? true : false;
                    this._menuSetting[MenuIndex.Application] = Convert.ToInt16(menuSettingStr[MenuIndex.Application]) == 1 ? true : false;
                    this._menuSetting[MenuIndex.Dashboard] = Convert.ToInt16(menuSettingStr[MenuIndex.Dashboard]) == 1 ? true : false;
                    this._menuSetting[MenuIndex.Device] = Convert.ToInt16(menuSettingStr[MenuIndex.Alert]) == 1 ? true : false;
                    this._menuSetting[MenuIndex.Sms] = Convert.ToInt16(menuSettingStr[MenuIndex.Sms]) == 1 ? true : false;
                    return _menuSetting;
                }
            }
            set
            {
                this._menuSetting = value;
                string[] s1 = new string[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    s1[i] = value[i] == true ? "1" : "0";
                }
                string menuStr = String.Join(",", s1);
                IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_MENU_SETTING, menuStr);
             
                Load();
            }
        }

        public Dictionary<String, int> DeviceColumnSetting
        {
            get
            {
                if (initialized)
                {
                    return _deviceColumnSetting;
                }
                else
                {
                    this._deviceColumnSetting = new Dictionary<string, int>();
                    string deviceStr = IniUtil.Instance().GetIniStringValue(SECTION_CLIENT, KEY_DEVICE_COLUMN_SETTING);
                    if (String.IsNullOrEmpty(deviceStr))
                    {
                        for (int i = 0; i < CelotMClient.CustomView.Device.columnsTitles.Length; i++)
                        {
                            this._deviceColumnSetting.Add(CelotMClient.CustomView.Device.columnsTitles[i], 1);
                        }
                        return this._deviceColumnSetting;
                    }
                  
                    string[] deviceColumnArrStr = deviceStr.Split(',');
                    for (int j = 0; j < deviceColumnArrStr.Length; j++)
                    {
                        string key = deviceColumnArrStr[j].Split(':')[0].Trim();
                        int result = Convert.ToInt32(deviceColumnArrStr[j].Split(':')[1].Trim());
                        Debug.WriteLine(String.Format("{0} : {1}", key, result));
                        this._deviceColumnSetting.Add(key, result);

                    }
                    return _deviceColumnSetting;
                }
            }
            set
            {
                
                this._deviceColumnSetting= value;
                string[] tmpArr = new string[value.Count];
                for (int i = 0; i < value.Count; i++)
                {
                    string key = value.Keys.ToList()[i];
                    int result = value.Values.ToList()[i];
                    string sString = String.Format("{0}:{1}", key, result);
                    tmpArr[i] = sString;
                }

                string finalStr = String.Join(",", tmpArr);
                IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_DEVICE_COLUMN_SETTING, finalStr);
                this.initialized = false;
                Load();
            }
        }

        public Dictionary<string, string> FirmwareDownloadInfo 
        {
            get 
            {
                if (initialized)
                {
                    return _fiemwareDownloadInfo;
                }
                else
                {
                    this._fiemwareDownloadInfo = new Dictionary<string, string>();
                    string firmwareInfo = IniUtil.Instance().GetIniStringValue(SECTION_CLIENT, KEY_FIRMWARE_DOWNLOAD_INFO);
                    if (!String.IsNullOrEmpty(firmwareInfo))
                    {
                        string[] firmInfoArrs = firmwareInfo.Split(',');
                        foreach(string infoStr in firmInfoArrs){
                            string key = infoStr.Split(':')[0];
                            string value = infoStr.Split(':')[1];
                            this._fiemwareDownloadInfo.Add(key, value);
                        }
                    }
                    else
                    {
                        _fiemwareDownloadInfo.Add("HOST_IP","10.164.195.72");
                        _fiemwareDownloadInfo.Add("VERSION","205");
                        _fiemwareDownloadInfo.Add("DOWNLOAD_FILE_NAME","");
                        _fiemwareDownloadInfo.Add("ADMIN_ID", "admin");
                        _fiemwareDownloadInfo.Add("ADMIN_PASSWORD", "admin");
                        _fiemwareDownloadInfo.Add("FTP_ID","admin");
                        _fiemwareDownloadInfo.Add("FTP_PASSWORD", "admin");
                        //다운로드 파일이름이 존재하지 않는다면 FIRMWARE_LENGTH 는 0으로 채워진다.
                        _fiemwareDownloadInfo.Add("FIRMWARE_LENGTH", "0");
                    }
                    return _fiemwareDownloadInfo;
                }
            }
            set 
            {
                if (value != null)
                {
                    this._fiemwareDownloadInfo = value;
                }
                if (this._fiemwareDownloadInfo != null && this._fiemwareDownloadInfo.Count > 0)
                {
                    int length = this._fiemwareDownloadInfo.Count;
                    string[] temp = new string[length];
                    for (int i = 0; i < length; i++)
                    {
                        string key = this._fiemwareDownloadInfo.Keys.ToList()[i];
                        string result = this._fiemwareDownloadInfo.Values.ToList()[i];
                        string sString = String.Format("{0}:{1}", key, result);
                        temp[i] = sString;
                    }

                    string finalStr = String.Join(",", temp);
                    IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_FIRMWARE_DOWNLOAD_INFO, finalStr);
                    this.initialized = false;
                    Load();

                }
            }
        }

        public Dictionary<String, int> AlertColumnSetting
        {
            get
            {
                if (initialized)
                {
                    return _alertColumnSetting;
                }
                else
                {
                    this._alertColumnSetting = new Dictionary<string, int>();
                    string alertStr = IniUtil.Instance().GetIniStringValue(SECTION_CLIENT, KEY_ALERT_COLUMN_SETTING);
                    if (String.IsNullOrEmpty(alertStr))
                    {
                        for (int i = 0; i < CelotMClient.CustomView.Alerts.columnsTitles.Length; i++)
                        {
                            this._alertColumnSetting.Add(CelotMClient.CustomView.Alerts.columnsTitles[i], 1);
                           
                        }
                        return this._alertColumnSetting;
                    }
                    string[] alertColumnArrStr = alertStr.Split(',');
                    for (int j = 0; j < alertColumnArrStr.Length; j++)
                    {
                        string key = alertColumnArrStr[j].Split(':')[0].Trim();
                        int result = Convert.ToInt32(alertColumnArrStr[j].Split(':')[1].Trim());
                        this._alertColumnSetting.Add(key, result);

                    }
                    return _alertColumnSetting;
                }
            }
            set
            {
                this._alertColumnSetting = value;
                string[] tmpArr = new string[value.Count];
                for (int i = 0; i < value.Count; i++)
                {
                    string key = value.Keys.ToList()[i];
                    int result = value.Values.ToList()[i];
                    string sString = String.Format("{0}:{1}", key, result);
                    tmpArr[i] = sString;
                }

                string finalStr = String.Join(",", tmpArr);
                IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_ALERT_COLUMN_SETTING, finalStr);
                this.initialized = false;
                Load();
            }
        }


        public string ServiceDisplayName
        {
            get
            {
                if (initialized)
                {
                    return _serviceDisplayName;
                }
                else
                {
                    _serviceDisplayName = IniUtil.Instance().GetIniStringValue(SECTION_SERVICE, KEY_SERVICE_DISPLAY_NAME);
                    return _serviceDisplayName;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_SERVICE, KEY_SERVICE_DISPLAY_NAME, value);
                this.initialized = false;
                Load();
            }
        }

        public string ServiceFilePath
        {
            get
            {
                if (initialized)
                {
                    return _serviceFilePath;
                }
                else
                {
                    _serviceFilePath = IniUtil.Instance().GetIniStringValue(SECTION_SERVICE, KEY_SERVICE_FILE_PATH);
                    return _serviceFilePath;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_SERVICE, KEY_SERVICE_FILE_PATH, value);
                this.initialized = false;
                Load();
            }
        }

        public int UsingProtocol
        {
            get
            {
                if (initialized)
                {
                    return _usingProtocol;
                }
                else
                {
                    _usingProtocol = IniUtil.Instance().GetIniIntValue(SECTION_PROTOCOL, KEY_USING__PROTOCOL);
                    return _usingProtocol;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_PROTOCOL, KEY_USING__PROTOCOL, value.ToString());
                this.initialized = false;
                Load();
            }
        }

        public int Install
        {
            get
            {
                if (initialized)
                {
                    return _install;
                }
                else
                {
                    _install = IniUtil.Instance().GetIniIntValue(SECTION_CLIENT, KEY_INSTALL);
                    return _install;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_INSTALL, value.ToString());
                this.initialized = false;
                Load();
            }
        }

        public int ServicePort
        {
            get
            {
                if (initialized)
                {
                    return _servicePort;
                }
                else
                {
                    _servicePort = IniUtil.Instance().GetIniIntValue(SECTION_SERVICE, KEY_SERVICE_PORT);
                    return _servicePort;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_SERVICE, KEY_SERVICE_PORT, value.ToString());
                this.initialized = false;
                Load();
            }
        }

        public bool NMSSupport
        {
            get
            {
                if (initialized)
                {
                    return _nmsSupport;
                }
                else
                {
                    int s = IniUtil.Instance().GetIniIntValue(SECTION_PROTOCOL, KEY_NMS_SUPPORT);
                    if (s == 1)
                    {
                        _nmsSupport = true;
                    }
                    else
                    {
                        _nmsSupport = false;
                    }
                    return _nmsSupport;
                }
            }
            set
            {
                int s = 0;
                if (value == true)
                {
                    s = 1;
                }
                IniUtil.Instance().SetIniValue(SECTION_PROTOCOL, KEY_NMS_SUPPORT, s.ToString());
                this.initialized = false;
                Load();
            }
        }

        public bool SNMPSupport
        {
            get
            {
                if (initialized)
                {
                    return _snmpSupport;
                }
                else
                {
                    int s = IniUtil.Instance().GetIniIntValue(SECTION_PROTOCOL, KEY_SNMP_SUPPORT);
                    if (s == 1)
                    {
                        _snmpSupport = true;
                    }
                    else
                    {
                        _snmpSupport = false;
                    }
                    return _snmpSupport;
                }
            }
            set
            {
                int s = 0;
                if (value == true)
                {
                    s = 1;
                }
                IniUtil.Instance().SetIniValue(SECTION_PROTOCOL, KEY_SNMP_SUPPORT, s.ToString());
                this.initialized = false;
                Load();
            }
        }


        public int UpdatePeriod
        {
            get
            {
                if (initialized)
                {
                    return _updatePeriod;
                }
                else
                {
                    _updatePeriod = IniUtil.Instance().GetIniIntValue(SECTION_CLIENT, KEY_UPDATE_PERIOD);
                    return _updatePeriod;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_UPDATE_PERIOD, value.ToString());
                this.initialized = false;
                Load();
            }
        }
        public int LowBatteryLimit
        {
            get
            {
                if (initialized)
                {
                    return _lowBatteryLimit;
                }
                else
                {
                    _lowBatteryLimit = IniUtil.Instance().GetIniIntValue(SECTION_CLIENT, KEY_LOW_BATTERY_LIMIT);
                    return _lowBatteryLimit;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_LOW_BATTERY_LIMIT, value.ToString());
                this.initialized = false;
                Load();

            }
        }

        public int NmsReportLimit {
            get
            {
                if (initialized)
                {
                    return _nmsReportLimit;
                }
                else
                {
                    _nmsReportLimit = IniUtil.Instance().GetIniIntValue(SECTION_CLIENT, KEY_NMS_REPORT_LIMIT);
                    if (_nmsReportLimit == 0) _nmsReportLimit = 30; 
                    return _nmsReportLimit;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_NMS_REPORT_LIMIT, value.ToString());
                this.initialized = false;
                Load();
            }
        
        
        }

        public int ConcurrentDownloadCount
        {
            get
            {
                if (initialized)
                {
                    return _concurrentDownloadCount;
                }
                else
                {
                    _concurrentDownloadCount = IniUtil.Instance().GetIniIntValue(SECTION_CLIENT, KEY_CONCURRENT_DOWNLOAD_COUNT);
                    return _concurrentDownloadCount;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_CONCURRENT_DOWNLOAD_COUNT, value.ToString());
                this.initialized = false;
                Load();
            }
        }

        public int RemotePort
        {
            get
            {
                if (initialized)
                {
                    return _remortPort;
                }
                else
                {
                    _remortPort = IniUtil.Instance().GetIniIntValue(SECTION_CLIENT, KEY_REMOTE_PORT);
                    return _remortPort;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_REMOTE_PORT, value.ToString());
                this.initialized = false;
                Load();
            }
        }


        public double Latitude
        {
            get
            {
                if (initialized)
                {
                    return _latitude;
                }
                else
                {

                    _latitude = "".Equals(IniUtil.Instance().GetIniStringValue(SECTION_CLIENT, KEY_LATITUDE)) ?
                        DEFAULT_LATITUDE : Convert.ToDouble(IniUtil.Instance().GetIniStringValue(SECTION_CLIENT, KEY_LATITUDE));
                    return _latitude;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_LATITUDE, value.ToString());
                this.initialized = false;
                Load();
            }
        }

        public double Longitude
        {
            get
            {
                if (initialized)
                {
                    return _longitude;
                }
                else
                {
                    _longitude = "".Equals(IniUtil.Instance().GetIniStringValue(SECTION_CLIENT, KEY_LONGITUDE)) ?
                        DEFAULT_LONGITUDE : Convert.ToDouble(IniUtil.Instance().GetIniStringValue(SECTION_CLIENT, KEY_LONGITUDE));
                    return _longitude;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_LONGITUDE, value.ToString());
                this.initialized = false;
                Load();
            }
        }

        public string ProtocolDes
        {
            get
            {
                if (initialized)
                {
                    return _protocolDes;
                }
                else
                {
                    _protocolDes = IniUtil.Instance().GetIniStringValue(SECTION_CLIENT, KEY_PROTOCOL_DES);
                    return _protocolDes;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_CLIENT, KEY_PROTOCOL_DES, value);
                this.initialized = false;
                Load();
            }
        }


        public string DatabaseHost
        {
            get
            {
                if (initialized)
                {
                    return _databaseHost;
                }
                else
                {
                    _databaseHost = IniUtil.Instance().GetIniStringValue(SECTION_DATABASE, KEY_DATABASE_HOST);
                    return _databaseHost;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_DATABASE, KEY_DATABASE_HOST, value);
                this.initialized = false;
                Load();
            }
        }

        public string DatabaseName
        {
            get
            {
                if (initialized)
                {
                    return _databaseName;
                }
                else
                {
                    _databaseName = IniUtil.Instance().GetIniStringValue(SECTION_DATABASE, KEY_DATABASE_NAME);
                    return _databaseName;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_DATABASE, KEY_DATABASE_NAME, value);
                this.initialized = false;
                Load();
            }
        }

        public string DatabaseId
        {
            get
            {
                if (initialized)
                {
                    return _databaseId;
                }
                else
                {
                    _databaseId = IniUtil.Instance().GetIniStringValue(SECTION_DATABASE, KEY_DATABASE_ID);
                    return _databaseId;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_DATABASE, KEY_DATABASE_ID, value);
                this.initialized = false;
                Load();
            }
        }

        public string DatabasePassword
        {
            get
            {
                if (initialized)
                {
                    return _databasePassword;
                }
                else
                {
                    _databasePassword = IniUtil.Instance().GetIniStringValue(SECTION_DATABASE, KEY_DATABASE_PASSWORD);
                    return _databasePassword;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_DATABASE, KEY_DATABASE_PASSWORD, value);
                this.initialized = false;
                Load();
            }
        }

        public string DatabaseCharset
        {
            get
            {
                if (initialized)
                {
                    return _databaseCharset;
                }
                else
                {
                    _databaseCharset = IniUtil.Instance().GetIniStringValue(SECTION_DATABASE, KEY_DATABASE_CHARSET);
                    return _databaseCharset;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_DATABASE, KEY_DATABASE_CHARSET, value);
                this.initialized = false;
                Load();
            }
        }

        public int MaxSessionCount
        {
            get
            {
                if (initialized)
                {
                    return _maxSessionCount;
                }
                else
                {
                    _maxSessionCount = IniUtil.Instance().GetIniIntValue(SECTION_SERVICE, KEY_MAX_SESSION_COUNT);
                    return _maxSessionCount;
                }
            }
            set
            {
                IniUtil.Instance().SetIniValue(SECTION_SERVICE, KEY_MAX_SESSION_COUNT, value.ToString());
                this.initialized = false;
                Load();
            }
        }
        #endregion



        
    }
}
