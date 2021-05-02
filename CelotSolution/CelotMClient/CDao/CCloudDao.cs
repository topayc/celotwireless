using CelotMClient.Dto;
using CelotMClient.Manager;
using CelotMClient.Model;
using CelotMClient.NMSStructure;
using CelotMClient.Util;
using CelotMClient.Worker;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.CDao
{
    public class CCloudDao : BaseDao
    {
        public CCloudDao(bool showProgress = true)
            : base()
        {
            this.showProgress = showProgress;
            this.EnableObjectAutoBind = true;
        }

        public CCloudDao(NotifyDataBaseFinishedHandler handler)
            : base(handler)
        {
            this.EnableObjectAutoBind = true;
        }

        public object GetPackektLogs(int identifier = 0, int startTime =0)
        {
            this.queryType = QueryType.Select;
            this.command.Parameters.Clear();
            
            string query = @"select * from packet_log WHERE  1 =1 ";
            //세션 번호가 0인 경우는 아래 구문이 적용되지 않아서 전체를 쿼리하게 됨 
            // 주석 처리 
            //if (identifier != 0)
                query += @" AND SessionId = " + identifier;

            if (startTime != 0)
                query += @" AND PacketLogRegDate >  " + startTime;

            Debug.WriteLine(query);
            this.command.CommandText = query;
            if (this.async)
            {
                this.worker.DoWork += new DoWorkEventHandler(GetPackets_DoWork);
                this.Query();
            }
            else
            {

            }
            return null;
        }

        private void GetPackets_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                nms_reprot_t nms;
                reader = this.command.ExecuteReader();
                if (!EnableObjectAutoBind)
                {
                    this.result = reader;
                    return;
                }

                List<DeviceChartDto> chartDtoList = new List<DeviceChartDto>();
                NMSAlertManager alertManage = new NMSAlertManager();
                byte[] rawPacketBuffer = new byte[815];
                while (reader.Read())
                {
                    nms = new nms_reprot_t();
                    reader.GetBytes(reader.GetOrdinal("RawPacket"), 0, rawPacketBuffer, 0, Marshal.SizeOf(nms));
                    unsafe
                    {
                        fixed (byte* fixedBuffer = rawPacketBuffer)
                        {
                            Marshal.PtrToStructure((IntPtr)fixedBuffer, nms);
                        }
                    }

                    DeviceChartDto chartDto = new DeviceChartDto
                    {
                        Time = CelotUtility.ChangeStampStringToLocalFormat(nms.data.current_time),
                        Tech = nms.data.moduleband,
                        Tx = nms.data.use_tx_amount,
                        Rx = nms.data.use_rx_amount,
                         RssiLevel = nms.data.modulesignal,
                       // RssiLevel = alertManage.getRssiLevel(nms.data.modulesignal);
                    };
                    chartDtoList.Add(chartDto); 
                }
                this.succeed = true;
                this.result = chartDtoList;
            }
            catch (Exception ee)
            {
                this.message = ee.Message;
                this.succeed = false;
            }
        }

        public object GetNMSLogReportCommandList(int identifier = 0)
        {
            this.queryType = QueryType.Select;
            this.command.Parameters.Clear();

            string query = @"
             SELECT * FROM
               (
                  (
                     SELECT 
                        D.*,
                        P.SessionId,
                        P.RawPacket
                    FROM packet_log AS P
                    LEFT OUTER JOIN device AS D
                        ON P.SessionId = D.PhoneNumber
                    ORDER BY D.DeviceNo ASC
                    )
                    UNION
                    (
                    SELECT 
                        D.*,
                        P.SessionId,
                        P.RawPacket
                    FROM packet_log AS P
                    RIGHT OUTER JOIN device AS D
                        ON P.SessionId = D.PhoneNumber
                    ORDER BY D.DeviceNo ASC
                    ) 
                ) AS R
            ";
            if (identifier != 0)
            {
                query += @" Where R.SessionId = " + identifier;
            }
          //  Debug.WriteLine("###################실행 쿼리#########################");
          //  Debug.WriteLine(query);
           this.command.CommandText = query;
            if (this.async)
            {
                this.worker.DoWork += new DoWorkEventHandler(NMSLogReportCommandList_DoWork);
                this.Query();
            }
            else
            {

            }
            return null;
        }

        private void NMSLogReportCommandList_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Type type = Type.GetType("CelotMClient.Model.Device");
                List<NMSReportCommand> nmsReportCommandList = new List<NMSReportCommand>();
                RowMapper rowMapper = new RowMapper();
                this.reader = this.command.ExecuteReader();
                byte[] rawPacketBuffer = new byte[815];

                while (reader.Read())
                {
                    Device device = null;
                    nms_reprot_t nms = null;

                    if (!DBNull.Value.Equals(reader["PhoneNumber"]))
                        device = (Device)rowMapper.SetInstance(reader, type);

                    if (!DBNull.Value.Equals(reader["RawPacket"]))
                    {
                        nms = new nms_reprot_t();
                        reader.GetBytes(reader.GetOrdinal("RawPacket"), 0, rawPacketBuffer, 0, Marshal.SizeOf(nms));
                        unsafe
                        {
                            fixed (byte* fixedBuffer = rawPacketBuffer)
                            {
                                Marshal.PtrToStructure((IntPtr)fixedBuffer, nms);
                            }
                        }
                    }
                    NMSReportCommand nmsReportCommand = new NMSReportCommand(device, nms);
                    nmsReportCommandList.Add(nmsReportCommand);
                }
                NMSDataWrapper dataWrapper = new NMSDataWrapper { NMSReportCommandList = nmsReportCommandList };
                dataWrapper.CheckAlert(false);

                this.succeed = true;
                this.result = dataWrapper;
            }
            catch (Exception ee)
            {
                this.message = ee.Message;
                this.succeed = false;
                Debug.WriteLine("######DB 에러");
                Debug.WriteLine(ee.Message);
            }
        }


        public object GetDevices(int sessionId = 0)
        {
            this.command.Parameters.Clear();
            this.queryType = QueryType.Select;
            string query = @"select * from packet_log WHERE  1 =1 ";
            if (sessionId != 0)
            {
                query += @" and PhoneNumber = " + sessionId;
            }
            string commandText = query;
            this.command.CommandText = commandText;
            if (this.async)
            {
                this.worker.DoWork += new DoWorkEventHandler(GetDevices_DoWork);
                this.Query();
            }
            else
            {

            }
            return null;
        }

        private void GetDevices_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                RowMapper mapper = new RowMapper();
                List<Device> list = new List<Device>();
                Type type = this.type = Type.GetType("CelotMClient.Model.NMS.Device");

                reader = this.command.ExecuteReader();
                if (!EnableObjectAutoBind)
                {
                    this.result = reader;
                    return;
                }

                while (reader.Read())
                {
                    list.Add((Device)mapper.SetInstance(reader, type));
                }
                this.result = list;
                this.succeed = true;
            }
            catch (Exception ee)
            {
                this.message = ee.Message;
                this.succeed = false;
            }
        }

        public object ModifyDevice(int sessionId,Device device)
        {
            this.command.Parameters.Clear();
            this.queryType = QueryType.update;

            string commandText =
              @"UPDATE device 
                    SET 
                        GroupName = @GroupName,
                        Name = @Name,
                        SerialNo = @SerialNo,
                        SecuCode = @SecuCode,
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
                    PhoneNumber = @SessionId
                ";
            this.command.CommandText = commandText;

            this.command.Parameters.Add("@GroupName", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Name", MySqlDbType.VarChar);
            this.command.Parameters.Add("@SerialNo", MySqlDbType.Int32);
            this.command.Parameters.Add("@SecuCode", MySqlDbType.VarChar);
            this.command.Parameters.Add("@PhoneNumber", MySqlDbType.Int32);
            this.command.Parameters.Add("@RouterIp", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Latitude", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Longitude", MySqlDbType.VarChar);
            this.command.Parameters.Add("@Des", MySqlDbType.Text);
            this.command.Parameters.Add("@SmsSupport", MySqlDbType.Int32);
            this.command.Parameters.Add("@BatterySupport", MySqlDbType.Int32);
            this.command.Parameters.Add("@WifiSupport", MySqlDbType.Int32);
            this.command.Parameters.Add("@VpnSupport", MySqlDbType.Int32);
            this.command.Parameters.Add("@SessionId", MySqlDbType.Int32);

            this.command.Parameters["@GroupName"].Value = device.GroupName;
            this.command.Parameters["@Name"].Value = device.Name;
            this.command.Parameters["@SerialNo"].Value = device.SerialNo;
            this.command.Parameters["@SecuCode"].Value = device.SecuCode;
            this.command.Parameters["@PhoneNumber"].Value = device.PhoneNumber;
            this.command.Parameters["@RouterIp"].Value = device.RouterIp;
            this.command.Parameters["@Latitude"].Value = device.Latitude;
            this.command.Parameters["@Longitude"].Value = device.Longitude;
            this.command.Parameters["@Des"].Value = device.Des;
            this.command.Parameters["@SmsSupport"].Value = device.SmsSupport;
            this.command.Parameters["@BatterySupport"].Value = device.BatterySupport;
            this.command.Parameters["@WifiSupport"].Value = device.WifiSupport;
            this.command.Parameters["@VpnSupport"].Value = device.VpnSupport;
            this.command.Parameters["@SessionId"].Value = sessionId;
            if (this.async)
            {
                this.worker.DoWork += new DoWorkEventHandler(DeviceAffected_DoWork);
                this.Query();
            }
            else
            {

            }
            return null;
        }

        public object CreateDevice(Device device)
        {
            this.command.Parameters.Clear();
            this.queryType = QueryType.Insert;
            string commandText =
                @"INSERT INTO 
                     device ( 
                        GroupName,
                        Name,
                        SerialNo,
                        SecuCode,
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
                        @SecuCode,
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
            this.command.Parameters.Add("@SecuCode", MySqlDbType.VarChar);
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
            this.command.Parameters["@SecuCode"].Value = device.SecuCode;
            this.command.Parameters["@PhoneNumber"].Value = device.PhoneNumber;
            this.command.Parameters["@RouterIp"].Value = device.RouterIp;
            this.command.Parameters["@Latitude"].Value = device.Latitude;
            this.command.Parameters["@Longitude"].Value = device.Longitude;
            this.command.Parameters["@Des"].Value = device.Des;
            this.command.Parameters["@SmsSupport"].Value = device.SmsSupport;
            this.command.Parameters["@BatterySupport"].Value = device.BatterySupport;
            this.command.Parameters["@WifiSupport"].Value = device.WifiSupport;
            this.command.Parameters["@VpnSupport"].Value = device.VpnSupport;
            this.command.Parameters["@DeviceRegDate"].Value = (int)((DateTime.Now.ToLocalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
            if (this.async)
            {
                this.worker.DoWork += new DoWorkEventHandler(DeviceAffected_DoWork);
                this.Query();
            }
            else
            {

            }
            return null;
        }

        public object DeleteDevice(int sessionId)
        {
            this.command.Parameters.Clear();
            this.queryType = QueryType.Delete;
            string commandText = "DELETE FROM device WHERE PhoneNumber = @SessionId";
            this.command.CommandText = commandText;
            this.command.Parameters.Add("@SessionId", MySqlDbType.Int32);
            this.command.Parameters["@SessionId"].Value = sessionId;
            if (this.async)
            {
                this.worker.DoWork += new DoWorkEventHandler(DeviceAffected_DoWork);
                this.Query();
            }
            else
            {

            }
            return null;
        }

        public int RawAffectedQuery(string query)
        {
            int affectedRows = 0; 

            this.queryType = QueryType.Insert;
            this.command.CommandText = query;
            if (this.async)
            {
                this.worker.DoWork += new DoWorkEventHandler(DeviceAffected_DoWork);
                this.Query();
            }
            else
            {

            }
            return affectedRows;
        }

        private void DeviceAffected_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.command.ExecuteNonQuery();
                this.succeed = true;
            }
            catch (Exception ee)
            {
                this.message = ee.Message;
                MessageBox.Show(ee.Message);
                this.succeed = false;
            }
            return;
        }
    }
}
