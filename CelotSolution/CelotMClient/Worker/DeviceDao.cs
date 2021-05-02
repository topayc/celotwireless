using CelotMClient.Manager;
using CelotMClient.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CelotMClient.Worker
{

    class DeviceDao : BaseWorker
    {

        public DeviceDao() : base() {
            this.EnableRowBind = true;
        }

        public DeviceDao(NotifyDBfinishedHandler handler): base(handler) {
            this.EnableRowBind = true;
        }

        public void select()
        {

            //worker.RunWorkerAsync();
            //base.showAletDialog();
        }

        public void GetDeviceReportRecords(int deviceNo,int phoneNumber, string startTime)
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.SelectRows;
            this.type = Type.GetType("CelotMClient.Model.DeviceReportCommand");
            this.command.CommandText = @"
                 SELECT 
                    D.*,
                    CDR.*,
                    CASE
                        WHEN CDR.ModuleSignal <= -105 THEN 1   
                        WHEN CDR.ModuleSignal >= -104 AND CDR.ModuleSignal <= -95 THEN 2 
                        WHEN CDR.ModuleSignal >= -94 AND CDR.ModuleSignal <= -90 THEN 3 
                        WHEN CDR.ModuleSignal >= -89 THEN 4 
                        ELSE 0
                    END AS RssiLevel
                FROM device_report_record AS CDR 
                LEFT OUTER JOIN device AS D
                    ON CDR.SessionId = D.PhoneNumber
                Where (D.PhoneNumber = @PhoneNumber OR CDR.SessionId = @PhoneNumber) AND STRCMP(CDR.CurrentTime, @StartTime) != -1
                ORDER BY CDR.CurrentTime ASC
            ";
            this.command.Parameters.Add("@PhoneNumber", MySqlDbType.Int32);
            this.command.Parameters.Add("@StartTime", MySqlDbType.VarChar);
            this.command.Parameters["@PhoneNumber"].Value = phoneNumber;
            this.command.Parameters["@StartTime"].Value = startTime;
            this.Query();
        }

        internal void GetDeviceReportRecords(int phoneNumber)
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.SelectRows;
            this.type = Type.GetType("CelotMClient.Model.DeviceReportCommand");
            this.command.CommandText = @"
                 SELECT 
                    D.*,
                    CDR.*,
                    CASE
                        WHEN CDR.ModuleSignal <= -105 THEN 1   
                        WHEN CDR.ModuleSignal >= -104 AND CDR.ModuleSignal <= -95 THEN 2 
                        WHEN CDR.ModuleSignal >= -94 AND CDR.ModuleSignal <= -90 THEN 3 
                        WHEN CDR.ModuleSignal >= -89 THEN 4 
                        ELSE 0
                    END AS RssiLevel
                FROM device_report_record AS CDR 
                LEFT OUTER JOIN device AS D
                    ON CDR.SessionId = D.PhoneNumber
                Where D.PhoneNumber = @PhoneNumber OR CDR.SessionId = @PhoneNumber
                ORDER BY CDR.CurrentTime ASC
            ";
            this.command.Parameters.Add("@PhoneNumber", MySqlDbType.Int32);
            this.command.Parameters["@PhoneNumber"].Value = phoneNumber;
            this.Query();
        }

        public void GetDeivceCurReportCommands()
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.SelectRows;
            this.type = Type.GetType("CelotMClient.Model.DeviceReportCommand");
            this.command.CommandText = @"
                (
                 SELECT 
                    D.*,
                    CDR.*,
                    CASE
                        WHEN CDR.ModuleSignal <= -105 THEN 1   
                        WHEN CDR.ModuleSignal >= -104 AND CDR.ModuleSignal <= -95 THEN 2 
                        WHEN CDR.ModuleSignal >= -94 AND CDR.ModuleSignal <= -90 THEN 3 
                        WHEN CDR.ModuleSignal >= -89 THEN 4 
                        ELSE 0
                    END AS RssiLevel
                FROM cur_device_report AS CDR 
                LEFT OUTER JOIN device AS D
                    ON CDR.SessionId = D.PhoneNumber
                ORDER BY D.DeviceNo ASC
                )
                UNION 
                (
                SELECT 
                    D.*,
                    CDR.*,
                    CASE
                        WHEN CDR.ModuleSignal <= -105 THEN 1   
                        WHEN CDR.ModuleSignal >= -104 AND CDR.ModuleSignal <= -95 THEN 2 
                        WHEN CDR.ModuleSignal >= -94 AND CDR.ModuleSignal <= -90 THEN 3 
                        WHEN CDR.ModuleSignal >= -89 THEN 4 
                        ELSE 0
                    END AS RssiLevel
                FROM cur_device_report AS CDR 
                RIGHT OUTER JOIN device AS D
                    ON CDR.SessionId = D.PhoneNumber
                ORDER BY D.DeviceNo ASC
                ) 
            ";
            this.Query();
        }

        public void GetDeivceCurReportCommand(Device device)
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.SelectOne;
            this.type = Type.GetType("CelotMClient.Model.DeviceReportCommand");
            this.command.CommandText = @"
                (
                 SELECT 
                    D.*,
                    CDR.*,
                    CASE
                        WHEN CDR.ModuleSignal <= -105 THEN 1   
                        WHEN CDR.ModuleSignal >= -104 AND CDR.ModuleSignal <= -95 THEN 2 
                        WHEN CDR.ModuleSignal >= -94 AND CDR.ModuleSignal <= -90 THEN 3 
                        WHEN CDR.ModuleSignal >= -89 THEN 4 
                        ELSE 0
                    END AS RssiLevel
                FROM cur_device_report AS CDR 
                LEFT OUTER JOIN device AS D
                    ON CDR.SessionId = D.PhoneNumber
                ORDER BY D.DeviceNo ASC
                )
                UNION 
                (
                SELECT 
                    D.*,
                    CDR.*,
                    CASE
                        WHEN CDR.ModuleSignal <= -105 THEN 1   
                        WHEN CDR.ModuleSignal >= -104 AND CDR.ModuleSignal <= -95 THEN 2 
                        WHEN CDR.ModuleSignal >= -94 AND CDR.ModuleSignal <= -90 THEN 3 
                        WHEN CDR.ModuleSignal >= -89 THEN 4 
                        ELSE 0
                    END AS RssiLevel
                FROM cur_device_report AS CDR 
                RIGHT OUTER JOIN device AS D
                   ON CDR.SessionId = D.PhoneNumber
                ORDER BY D.DeviceNo ASC
                ) 
                WHERE CDR.SessionId = @PhoneNumber
            ";
            this.command.Parameters.Add("@PhoneNumber", MySqlDbType.Int32);
            this.command.Parameters["@PhoneNumber"].Value = device.PhoneNumber;
            this.Query();
        }

        public void GetDeivceReportRecordCommands(Device device)
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.SelectRows;
            this.type = Type.GetType("CelotMClient.Model.DeviceReportCommand");
            this.command.CommandText = @"
                SELECT 
                    D.*,
                    CDR.*
                FROM cur_device_report AS CDR 
                LEFT OUTER JOIN device AS D
                    ON CDR.RefDeviceNo = D.DeviceNo 
                Where CDR.SessionId = @PhoneNumber
            ";
            this.command.Parameters.Add("@PhoneNumber", MySqlDbType.Int32);
            this.command.Parameters["@PhoneNumber"].Value = device.PhoneNumber;
            this.Query();
        }

        public void GetDevices()
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.SelectRows;
            this.type = Type.GetType("CelotMClient.Model.Device");
            string commandText = "SELECT * FROM device ORDER BY DeviceNo ASC";
            this.command.CommandText = commandText;
            this.Query();
        }

        public void GetDevice(Device device)
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.SelectOne;
            this.type = Type.GetType("CelotMClient.Model.Device");
            string commandText = "SELECT * FROM Device WHERE PhoneNumber = @PhoneNumber";
            this.command.CommandText = commandText;

            this.command.Parameters.Add("@PhoneNumber", MySqlDbType.Int32);
            this.command.Parameters["@PhoneNumber"].Value = device.PhoneNumber;
            this.Query();
        }

        public void GetTechnologyList()
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.SelectRows;
            this.type = Type.GetType("CelotMClient.Model.NameValuePair");
            string commandText = @"SELECT ModuleBand AS Name, COUNT(ModuleBand) AS Value FROM cur_device_report GROUP BY ModuleBand";
            this.command.CommandText = commandText;
            this.Query();
        }

      

        public void GetRssiLevelList()
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.SelectRows;
            this.type = Type.GetType("CelotMClient.Model.NameValuePair");
            string commandText =
                @"SELECT 
                    T.Value AS Name,
                    COUNT(T.Value) AS Value 
                  FROM 
                    (
                     SELECT 
                        CASE
                        WHEN ModuleSignal = 0 THEN 0
                        WHEN ModuleSignal <= -105 THEN 1   
                        WHEN ModuleSignal >= -104 AND ModuleSignal <= -95 THEN 2 
                        WHEN ModuleSignal >= -95 AND ModuleSignal <= -90 THEN 3 
                        WHEN ModuleSignal >= -89 THEN 4 
                        ELSE -1
                        END AS Value
                     From cur_device_report
                    ) AS T
                   group by Name
                ";
            this.command.CommandText = commandText;
            this.Query();
        }

        public void ModifyDevice(Device device)
        {
            Debug.WriteLine("device 번호  : {0}", device.DeviceNo);
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.Update;
            string commandText =
              @"UPDATE device 
                    SET 
                        GroupName = @GroupName,
                        Name = @Name,
                        SerialNo = @SerialNo,
                        PhoneNumber = @PhoneNumber,
                        RouterIp = @RouterIp,
                        Latitude = @Latitude,
                        Longitude = @Longitude,
                        Des = @Des,
                        SmsSupport = @SmsSupport,
                        BatterySupport = @BatterySupport,
                        WifiSupport = @WifiSupport,
                        VpnSupport = @VpnSupport
                  where 
                    DeviceNo = @DeviceNo
                ";
            this.command.CommandText = commandText;

            this.command.Parameters.Add("@GroupName", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Name", MySqlDbType.VarChar);
            this.command.Parameters.Add("@SerialNo", MySqlDbType.Int32);
            this.command.Parameters.Add("@PhoneNumber", MySqlDbType.Int32);
            this.command.Parameters.Add("@RouterIp", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Latitude", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Longitude", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Des", MySqlDbType.Text);
            this.command.Parameters.Add("@SmsSupport", MySqlDbType.Int32);
            this.command.Parameters.Add("@BatterySupport", MySqlDbType.Int32);
            this.command.Parameters.Add("@WifiSupport", MySqlDbType.Int32);
            this.command.Parameters.Add("@VpnSupport", MySqlDbType.Int32);
            this.command.Parameters.Add("@DeviceNo", MySqlDbType.Int32);

            this.command.Parameters["@GroupName"].Value = device.GroupName;
            this.command.Parameters["@Name"].Value = device.Name;
            this.command.Parameters["@SerialNo"].Value = device.SerialNo;
            this.command.Parameters["@PhoneNumber"].Value = device.PhoneNumber;
            this.command.Parameters["@RouterIp"].Value = device.RouterIp;
            this.command.Parameters["@Latitude"].Value = device.Latitude;
            this.command.Parameters["@Longitude"].Value = device.Longitude;
            this.command.Parameters["@Des"].Value = device.Des;
            this.command.Parameters["@SmsSupport"].Value = device.SmsSupport;
            this.command.Parameters["@BatterySupport"].Value = device.BatterySupport;
            this.command.Parameters["@WifiSupport"].Value = device.WifiSupport;
            this.command.Parameters["@VpnSupport"].Value = device.VpnSupport;
            this.command.Parameters["@DeviceNo"].Value = device.DeviceNo;
            this.Query();
        }

        public void CreateDevice(Device device)
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.Insert;
            string commandText =
                @"INSERT INTO 
                     device ( 
                        GroupName,
                        Name,
                        SerialNo,
                        PhoneNumber,
                        RouterIp,
                        Latitude,
                        Longitude,
                        Des,
                        SmsSupport,
                        BatterySupport,
                        WifiSupport,
                        VpnSupport,
                        DeviceRegDate
                     ) 
                     VALUES(
                        @GroupName,
                        @Name,
                        @SerialNo,
                        @PhoneNumber,
                        @RouterIp,
                        @Latitude,
                        @Longitude,
                        @Des,
                        @SmsSupport,
                        @BatterySupport,
                        @WifiSupport,
                        @VpnSupport,
                        @DeviceRegDate
                      )";
            this.command.CommandText = commandText;

            this.command.Parameters.Add("@GroupName", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Name", MySqlDbType.VarChar);
            this.command.Parameters.Add("@SerialNo", MySqlDbType.Int32);
            this.command.Parameters.Add("@PhoneNumber", MySqlDbType.Int32);
            this.command.Parameters.Add("@RouterIp", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Latitude", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Longitude", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Des", MySqlDbType.Text);
            this.command.Parameters.Add("@SmsSupport", MySqlDbType.Int32);
            this.command.Parameters.Add("@BatterySupport", MySqlDbType.Int32);
            this.command.Parameters.Add("@WifiSupport", MySqlDbType.Int32);
            this.command.Parameters.Add("@VpnSupport", MySqlDbType.Int32);
            this.command.Parameters.Add("@DeviceRegDate", MySqlDbType.Int32);

            this.command.Parameters["@GroupName"].Value = device.GroupName;
            this.command.Parameters["@Name"].Value = device.Name;
            this.command.Parameters["@SerialNo"].Value = device.SerialNo;
            this.command.Parameters["@PhoneNumber"].Value = device.PhoneNumber;
            this.command.Parameters["@RouterIp"].Value = device.RouterIp;
            this.command.Parameters["@Latitude"].Value = device.Latitude;
            this.command.Parameters["@Longitude"].Value = device.Longitude;
            this.command.Parameters["@Des"].Value = device.Des;
            this.command.Parameters["@SmsSupport"].Value = device.SmsSupport;
            this.command.Parameters["@BatterySupport"].Value = device.BatterySupport;
            this.command.Parameters["@WifiSupport"].Value = device.WifiSupport;
            this.command.Parameters["@VpnSupport"].Value = device.VpnSupport;
            this.command.Parameters["@DeviceRegDate"].Value = DateTime.Now.Millisecond;
            this.Query();
        }

        public void DeleteDevice(Device device)
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.Delete;
            string commandText = "DELETE FROM device WHERE DeviceNo = @DeviceNo";
            this.command.CommandText = commandText;
            this.command.Parameters.Add("@DeviceNo", MySqlDbType.Int32);
            this.command.Parameters["@DeviceNo"].Value = device.DeviceNo;
            this.Query();
        }

        public void GetDeivceAlertStatistics(int sessionId)
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.SelectRows;
            this.EnableRowBind = false;
            int lowBatteryLimit = ApplicationConfig.Instance().LowBatteryLimit;
            string commandText =
                 @"SELECT 
                    SUM(CASE WHEN DRP.Ethernet1State = 1 THEN 1 ELSE 0 END) AS Lan1ErrCount,
                    SUM(CASE WHEN DRP.Ethernet2State = 1 THEN 1 ELSE 0 END) AS Lan2ErrCount, 
                    SUM(CASE WHEN DRP.NetworkState = 0 THEN 1 ELSE 0 END) AS NetworkErrCount, 
                    SUM(CASE WHEN DRP.ExternalPower = 1 THEN 1 ELSE 0 END) AS ExternalPowerErrCount,
                    SUM(CASE WHEN DRP.RssiLevel <= 1 THEN 1 ELSE 0 END) AS RssiLevelErrCount,
                    SUM(CASE WHEN SUBSTRING_INDEX(SUBSTRING_INDEX(DRP.ExtDEvice1, ',', 1), ';', -1) <= @LowBatteryLimit THEN 1 ELSE 0 END) AS LowBatteryErrCount,
                    SUM(
                        CASE 
                            WHEN DRP.PhoneNumber IS NULL AND DRP.SessionId IS NOT NULL THEN 1 
                            WHEN DRP.RouterIp != DRP.CurrentIpAddress THEN 1
                            WHEN DRP.WifiSupport  = 0 
                                THEN
                                    CASE
                                        WHEN DRP.WifiStatus != 0 AND DRP.WifiStatus != 1 THEN 1 ELSE 0 
                                    END 
                            WHEN DRP.VpnSupport = 0 
                                THEN 
                                    CASE 
                                        WHEN DRP.VpnStatus != 0 AND DRP.VpnStatus != 1 THEN 1 ELSE 0
                                    END 
                        END ) AS DeviceIdErrCount
                 FROM 
                   (
                    (
                     SELECT 
                        D.*,
                        CDR.*,
                        CASE
                            WHEN CDR.ModuleSignal <= -105 THEN 1   
                            WHEN CDR.ModuleSignal >= -104 AND CDR.ModuleSignal <= -95 THEN 2 
                            WHEN CDR.ModuleSignal >= -94 AND CDR.ModuleSignal <= -90 THEN 3 
                            WHEN CDR.ModuleSignal >= -89 THEN 4 
                            ELSE 0
                        END AS RssiLevel
                    FROM device_report_record AS CDR 
                    LEFT OUTER JOIN device AS D
                        ON CDR.SessionId = D.PhoneNumber
                    ORDER BY D.DeviceNo ASC
                    )
                    UNION 
                    (
                    SELECT 
                        D.*,
                        CDR.*,
                        CASE
                            WHEN CDR.ModuleSignal <= -105 THEN 1   
                            WHEN CDR.ModuleSignal >= -104 AND CDR.ModuleSignal <= -95 THEN 2 
                            WHEN CDR.ModuleSignal >= -94 AND CDR.ModuleSignal <= -90 THEN 3 
                            WHEN CDR.ModuleSignal >= -89 THEN 4 
                            ELSE 0
                        END AS RssiLevel
                    FROM device_report_record AS CDR 
                    RIGHT OUTER JOIN device AS D
                        ON CDR.SessionId = D.PhoneNumber
                    ORDER BY D.DeviceNo ASC
                  )
                ) AS DRP 
                  WHERE DRP.SessionId = @SessionId OR DRP.PhoneNUmber = @SessionId
                ";
            this.command.CommandText = commandText;
            this.command.Parameters.Add("@LowBatteryLimit", MySqlDbType.Int32);
            this.command.Parameters.Add("@SessionId", MySqlDbType.Int32);

            this.command.Parameters["@LowBatteryLimit"].Value = lowBatteryLimit;
            this.command.Parameters["@SessionId"].Value = sessionId;
            this.Query();
        }

        internal void GetAllDeivceAlertStatistics()
        {
            this.command.Parameters.Clear();
            this.queryMode = QueryMode.SelectRows;
            this.EnableRowBind = false;
            int lowBatteryLimit = ApplicationConfig.Instance().LowBatteryLimit;
            string commandText =
                 @"SELECT 
                    SUM(CASE WHEN DRP.Ethernet1State = 1 THEN 1 ELSE 0 END) AS Lan1ErrCount,
                    SUM(CASE WHEN DRP.Ethernet2State = 1 THEN 1 ELSE 0 END) AS Lan2ErrCount, 
                    SUM(CASE WHEN DRP.NetworkState = 0 THEN 1 ELSE 0 END) AS NetworkErrCount, 
                    SUM(CASE WHEN DRP.ExternalPower = 1 THEN 1 ELSE 0 END) AS ExternalPowerErrCount,
                    SUM(CASE WHEN DRP.RssiLevel <= 1 THEN 1 ELSE 0 END) AS RssiLevelErrCount,
                    SUM(CASE WHEN SUBSTRING_INDEX(SUBSTRING_INDEX(DRP.ExtDEvice1, ',', 1), ';', -1) <= @LowBatteryLimit THEN 1 ELSE 0 END) AS LowBatteryErrCount,
                    SUM(
                        CASE 
                            WHEN DRP.PhoneNumber IS NULL AND DRP.SessionId IS NOT NULL THEN 1 
                            WHEN DRP.RouterIp != DRP.CurrentIpAddress THEN 1
                            WHEN DRP.WifiSupport  = 0 
                                THEN
                                    CASE
                                        WHEN DRP.WifiStatus != 0 AND DRP.WifiStatus != 1 THEN 1 ELSE 0 
                                    END 
                            WHEN DRP.VpnSupport = 0 
                                THEN 
                                    CASE 
                                        WHEN DRP.VpnStatus != 0 AND DRP.VpnStatus != 1 THEN 1 ELSE 0
                                    END 
                        END ) AS DeviceIdErrCount
                 FROM 
                   (
                    (
                     SELECT 
                        D.*,
                        CDR.*,
                        CASE
                            WHEN CDR.ModuleSignal <= -105 THEN 1   
                            WHEN CDR.ModuleSignal >= -104 AND CDR.ModuleSignal <= -95 THEN 2 
                            WHEN CDR.ModuleSignal >= -94 AND CDR.ModuleSignal <= -90 THEN 3 
                            WHEN CDR.ModuleSignal >= -89 THEN 4 
                            ELSE 0
                        END AS RssiLevel
                    FROM device_report_record AS CDR 
                    LEFT OUTER JOIN device AS D
                        ON CDR.SessionId = D.PhoneNumber
                    ORDER BY D.DeviceNo ASC
                    )
                    UNION 
                    (
                    SELECT 
                        D.*,
                        CDR.*,
                        CASE
                            WHEN CDR.ModuleSignal <= -105 THEN 1   
                            WHEN CDR.ModuleSignal >= -104 AND CDR.ModuleSignal <= -95 THEN 2 
                            WHEN CDR.ModuleSignal >= -94 AND CDR.ModuleSignal <= -90 THEN 3 
                            WHEN CDR.ModuleSignal >= -89 THEN 4 
                            ELSE 0
                        END AS RssiLevel
                    FROM device_report_record AS CDR 
                    RIGHT OUTER JOIN device AS D
                        ON CDR.SessionId = D.PhoneNumber
                    ORDER BY D.DeviceNo ASC
                  )
                ) AS DRP 
                ";
            this.command.CommandText = commandText;
            this.command.Parameters.Add("@LowBatteryLimit", MySqlDbType.Int32);

            this.command.Parameters["@LowBatteryLimit"].Value = lowBatteryLimit;
            this.Query();
        }
    }
}
