using CelotMClient.Model;
using CelotMClient.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Diagnostics;
using CelotMClient.NMSStructure;
using System.Timers;
using System.Runtime.InteropServices;
using CelotMClient.CDao;
using System.Threading;
using CelotMClient.CustomForm;

namespace CelotMClient.Manager
{
    public delegate void NMSDataNotiHandler(object sender, NMSDataNotifyEventArgs args);

     public class NMSDataNotifyEventArgs : EventArgs
    {
        public NMSDataWrapper Data
        {
            get;
            set;
        }

        public bool Result
        {
            get;
            set;
        }
    }

    public class ApplicationCache
    {
        private static ApplicationCache _cacheManager;
        private List<Device> _deviceList;

        byte[] rawPacketBuffer1 = new byte[815];
        byte[] rawPacketBuffer2 = new byte[815];

        private NMSDataWrapper _curNMSDataWrapper;
        private NMSDataWrapper _logNMSDataWrapper;

        private System.Timers.Timer _timer;
        private static bool isInit = false;

        public event NMSDataNotiHandler NMSDataNotify; 

        private ApplicationCache() { }
        public static ApplicationCache Instance()
        {
            if (_cacheManager == null)
            {
                _cacheManager = new ApplicationCache();
                _cacheManager.Devices = new List<Device>();
                if (!isInit)
                {
                    _cacheManager.Init();
                }
            }
            return _cacheManager;
        }

    
        public string CheckRegisterdDevice(Device d, out bool result)
        {
            result = true;
            StringBuilder strBuilder = new StringBuilder();
            // 종북 체크에서 router ip 는 제외
            //int ipCount        = Devices.Where(rd => rd.RouterIp.Equals(d.RouterIp)).Count();
            int serialCount = Devices.Where(rd => rd.SerialNo == d.SerialNo).Count();
            int phoneCount = Devices.Where(rd => rd.PhoneNumber == d.PhoneNumber).Count();

            /*
            if (ipCount > 0)
            {
                result = false;
                strBuilder.Append(String.Format("[ERROR] : already registered router IP ({0})  ", d.RouterIp));
            }
            */

            if (serialCount > 0)
            {
                result = false;
                strBuilder.Append(String.Format("[ERROR] : already registered serial no ({0})  ", d.SerialNo));
            }
            if (phoneCount > 0)
            {
                result = false;
                strBuilder.Append(String.Format("[ERROR]: already registered phone number ({0})  ", d.PhoneNumber));
            }
            return strBuilder.ToString();
        }

        public NMSDataWrapper CurNMSDataWrapper
        {
            get { return _curNMSDataWrapper; }
            set { _curNMSDataWrapper = value; }
        }

        public NMSDataWrapper LogNMSDataWrapper
        {
            get { return _logNMSDataWrapper; }
            set { _logNMSDataWrapper = value; }
        }

        public List<Device> Devices
        {
            get { return _deviceList; }
            set { _deviceList = value; }
        }

        public void SetTimerInterval(int interval)
        {
            this._timer.Stop();
            this._timer.Interval = interval;
            this._timer.Start();
        }

        public void Init()
        {
            this.LoadDevices();
            this.PrepareApplicationCacheData();
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
            _timer = new System.Timers.Timer();
            _timer.Interval = ApplicationConfig.Instance().UpdatePeriod;
            _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            _timer.Start();
        }

        //디바이스 정보는 사용자의 명시적 추가 삭제, 수정에 의해서 수정되기 때문에 특정 이벤트발생시에만 재로딩한다.
        public void LoadDevices()
        {
            if (_deviceList != null)
            {
                _deviceList.Clear();
            }
            else
            {
                _deviceList = new List<Device>();
            }

            Type type = Type.GetType("CelotMClient.Model.Device");
            RowMapper mapper = new RowMapper();
            MySqlConnection con = DatabaseManager.Instance().GetConnection();
            MySqlCommand command = new MySqlCommand("select * from device", con);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                _deviceList.Add((Device)mapper.SetInstance(reader, type));
            }
            isInit = true;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            PrepareApplicationCacheData();
        }

        public void PrepareApplicationCacheData()
        {
            LoadNMSReportCommandList();
            //LoadNMSReportCommandLogList();
        }

        //현재 디바이스 상태 패킷 정보를 로딩 
        public void LoadNMSReportCommandList()
        {
            List<NMSReportCommand> nmsReportCommandList = new List<NMSReportCommand>();
            Type type = Type.GetType("CelotMClient.Model.Device");
            RowMapper rowMapper = new RowMapper();

            MySqlConnection conn = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            string commandText = @"
                (
                 SELECT 
                    D.*,
                    P.RawPacket
                FROM packet AS P
                LEFT OUTER JOIN device AS D
                    ON P.SessionId = D.PhoneNumber
                ORDER BY D.DeviceNo ASC
                )
                UNION 
                (
                SELECT 
                    D.*,
                    P.RawPacket
                FROM packet AS P
                RIGHT OUTER JOIN device AS D
                    ON P.SessionId = D.PhoneNumber
                ORDER BY D.DeviceNo ASC
                ) 
            ";

            try
            {
                conn = DatabaseManager.Instance().GetConnection();
                command = new MySqlCommand(commandText, conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Device device = null;
                    nms_reprot_t nms = null;

                    if (!DBNull.Value.Equals(reader["PhoneNumber"]))
                        device = (Device)rowMapper.SetInstance(reader, type);

                    if (!DBNull.Value.Equals(reader["RawPacket"]))
                    {
                        nms = new nms_reprot_t();
                        reader.GetBytes(reader.GetOrdinal("RawPacket"), 0, rawPacketBuffer1, 0, Marshal.SizeOf(nms));
                        unsafe
                        {
                            fixed (byte* fixedBuffer = rawPacketBuffer1)
                            {
                                Marshal.PtrToStructure((IntPtr)fixedBuffer, nms);
                            }
                        }
                        //dumpNms(nms);
                    }
                    NMSReportCommand nmsReportCommand = new NMSReportCommand(device, nms);
                    nmsReportCommandList.Add(nmsReportCommand);
                }
                _curNMSDataWrapper = null;
                _curNMSDataWrapper = new NMSDataWrapper { NMSReportCommandList = nmsReportCommandList };
                _curNMSDataWrapper.CheckAlert(true);

                if (this.NMSDataNotify != null)
                {
                    this.NMSDataNotify(this, new NMSDataNotifyEventArgs { Data = this._curNMSDataWrapper });
                }
            }
            catch (Exception ee)
            {
                Debug.WriteLine("######### LoadNMSReportCommandList Error");
                Debug.WriteLine(ee.Message);
                Debug.WriteLine(ee.StackTrace);
            }
            finally
            {
                reader.Close();
                conn.Close();

            }
        }

        //현재까지의 모든 패킷 로그를 가져옴 
        public void LoadNMSReportCommandLogList()
        {
            List<NMSReportCommand> nmsReportCommandList = new List<NMSReportCommand>();
            Type type = Type.GetType("CelotMClient.Model.Device");
            RowMapper rowMapper = new RowMapper();

            MySqlConnection conn = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            string commandText = @"
                (
                 SELECT 
                    D.*,
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
                    P.RawPacket
                FROM packet_log AS P
                RIGHT OUTER JOIN device AS D
                    ON P.SessionId = D.PhoneNumber
                ORDER BY D.DeviceNo ASC
                ) 
            ";
            try
            {
                conn = DatabaseManager.Instance().GetConnection();
                command = new MySqlCommand(commandText, conn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Device device = null;
                    nms_reprot_t nms = null;

                    if (!DBNull.Value.Equals(reader["PhoneNumber"]))
                        device = (Device)rowMapper.SetInstance(reader, type);

                    if (!DBNull.Value.Equals(reader["RawPacket"]))
                    {
                        nms = new nms_reprot_t();
                        reader.GetBytes(reader.GetOrdinal("RawPacket"), 0, rawPacketBuffer2, 0, Marshal.SizeOf(nms));
                        unsafe
                        {
                            fixed (byte* fixedBuffer = rawPacketBuffer2)
                            {
                                Marshal.PtrToStructure((IntPtr)fixedBuffer, nms);
                            }
                        }
                        dumpNms(nms);
                    }
                    NMSReportCommand nmsReportCommand = new NMSReportCommand(device, nms);
                    nmsReportCommandList.Add(nmsReportCommand);
                }
                _logNMSDataWrapper = null;
                _logNMSDataWrapper = new NMSDataWrapper { NMSReportCommandList = nmsReportCommandList };
                _logNMSDataWrapper.CheckAlert(false);
            }
            catch (Exception ee)
            {
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        public void LoadDevicesAsync()
        {
            CCloudDao cDao = new CCloudDao();
            cDao.NotifyDataBaseFinished += new NotifyDataBaseFinishedHandler(Device_received);
            cDao.GetDevices();
        }

        private void Device_received(object Sender, DataBaseFinishedEventArgs e)
        {
            this._deviceList = (List<Device>)e.Result;
            isInit = true;
        }

        public bool IsRegisteredDeivceByRouterIp(string ip)
        {
            bool registered = false;
            foreach (Device device in _deviceList)
            {
                if (device.RouterIp.Equals(ip))
                {
                    registered = true;
                    break;
                }
            }
            return registered;
        }

        public bool IsRegisteredDeivceByPhoneNumber(int phoneNumber)
        {
            bool registered = false;
            foreach (Device device in _deviceList)
            {
                if (device.PhoneNumber.Equals(phoneNumber))
                {
                    registered = true;
                    break;
                }
            }
            return registered;
        }


        private void dumpNms(nms_reprot_t r)
        {
            //Debug.WriteLine(String.Format("[{0}]    [{1}]    [{2}]    [{3}]    [{4}]    [{5}]    [{6}]    [{7}]   ",
            //        r.header.session_id,
            //        r.header.message_type,
            //        r.header.data_len,
            //        r.data.current_ip_address,
            //        r.data.hw_version,
            //        r.data.sw_version,
            //        r.data.rpt_port,
            //        r.data.rmt_port
            //        ));
        }

        public void dumpNmsReportCommand(List<NMSReportCommand> nmsReportCommandList)
        {
            //Debug.WriteLine("");
            //Debug.WriteLine("############ NMS Report command  ############");
            //Debug.WriteLine("");
            // MessageBox.Show("개수" + nmsReportCommandList.Count.ToString());
            foreach (NMSReportCommand command in nmsReportCommandList)
            {

                //Debug.WriteLine(String.Format("{0}", command.Device.PhoneNumber));
                //Debug.WriteLine(String.Format("{0} ||  {1} ||  {2} ||  {3} ||  {4} ||  {5} ||  {6} ||  {7} ||  {8} ||  {9} || {10}  ",
                //command.Device.PhoneNumber,
                //command.Device.Latitude,
                //command.Device.Longitude,
                //command.Device.RouterIp,
                //command.Device.Name,
                //command.nms_reprot_t.header.session_id,
                //command.nms_reprot_t.data.hw_version,
                //command.nms_reprot_t.data.sw_version,
                //command.nms_reprot_t.data.current_ip_address,
                //command.GetLanIPString(),
                //command.GetWanIPString()
                //));
                //Debug.WriteLine("");
            }
        }

        public MainForm MainForm { get; set; }

        public List<NMSReportCommand> LogNMSReportcommandList { get; set; }
    }

}
