using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CelotMClient.Worker;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using CelotMClient.Manager;
using System.IO;
using System.Data.OleDb;
using CelotMClient.CustomForm;
using CelotMClient.CDao;
using CelotMClient.Util;
using System.Threading;

namespace CelotMClient.CustomView
{
    public partial class SettingsDetailUserControll : UserControl
    {
        private string deviceFileName;
        private int deviceBatchfileStatus = 0; // 0 파일선택 안됨 , 1 파일 선택됨     
        private MainForm mainForm;
    
        public SettingsDetailUserControll()
        {
            InitializeComponent();
        }

        public SettingsDetailUserControll(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void SettingsDetailUserControll_Load(object sender, EventArgs e)
        {
            this.InitControl();
            this.statusTimer.Enabled = true;
        }

        private void settingTimer_tick(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void InitControl()
        {
            ApplicationConfig config = ApplicationConfig.Instance();
            ServiceManager.Instance().SetServiceStatus();

            this.serviceStatusLabel.Text = ServiceManager.Instance().QueryServiceStatus().Parse();
            this.serviceBtn.Text = ServiceManager.Instance().GetNextStatusString();
            
            this.databaseStatusLabel.Text = DatabaseManager.Instance().DatabaseStatus.Parse();
            this.databseCheckBtn.Text = DatabaseManager.Instance().GetNextStatusString();
            
            this.updatePeriodLabel.Text = config.UpdatePeriod.ToString();
            this.lowBatteryLabel.Text = config.LowBatteryLimit.ToString();

            this.lowBatteryBox.Text = config.LowBatteryLimit.ToString();
            this.updatePeriodBox.Text = config.UpdatePeriod.ToString();
            this.dbHostBox.Text = config.DatabaseHost;
            this.dbNameBox.Text = config.DatabaseName;
            this.dbIdBox.Text = config.DatabaseId;
            this.dbPassBox.Text = config.DatabasePassword;
            this.dbCharsetBox.Text = config.DatabaseCharset;
            this.portBox.Text = ApplicationConfig.Instance().ServicePort.ToString();
            this.nmsLimitBox.Text = ApplicationConfig.Instance().NmsReportLimit.ToString();

            this.protocolDesLabel.Text = "[C-CLOUD]\n버젼 : v1.1.0\n날짜 : 2016년 1월 1일\nCopyright c 2010-2016 celotwiress All right reserved\nwww.celotwireless.com";
            //this.protocolDesLabel.Text = config.ProtocolDes;
            this.protocolCombo.SelectedIndex = 0;

            this.coordLabel.Text = String.Format("위도 : {0},  경도: {1}", ApplicationConfig.Instance().Latitude, ApplicationConfig.Instance().Longitude);
            this.setDeviceBatchBtnText();
        }

        
        private int tickCount = 0;
        private void statusTimer_Tick(object sender, EventArgs e)
        {
            if (DatabaseManager.Instance().DatabaseStatus == DatabaseStatus.NotConnected)
            {
                if (tickCount % 2 == 0)
                {
                    databaseStatusLabel.BackColor = Color.FromArgb(255, 65, 65, 65);
                    databaseStatusLabel.ForeColor = Color.FromArgb(255, 255, 255, 255);
                }
                else
                {
                    databaseStatusLabel.BackColor = Color.FromArgb(255, 220, 220, 220);
                    databaseStatusLabel.ForeColor = Color.FromArgb(255, 50, 50, 50);
                }
            }
            else
            {
                databaseStatusLabel.BackColor = Color.FromArgb(255, 65, 65, 65);
                databaseStatusLabel.ForeColor = Color.FromArgb(255, 255, 255, 255);
            }

            if (ServiceManager.Instance().ServiceStatus == CelotServiceStatus.NotInstalled
                || ServiceManager.Instance().ServiceStatus == CelotServiceStatus.Stopped
                )
            {
                if (tickCount % 2 == 0)
                {
                    this.serviceStatusLabel.BackColor = Color.FromArgb(255, 65, 65, 65);
                    this.serviceStatusLabel.ForeColor = Color.FromArgb(255, 255, 255, 255);
                }
                else
                {
                    this.serviceStatusLabel.BackColor = Color.FromArgb(255, 220, 220, 220);
                    this.serviceStatusLabel.ForeColor = Color.FromArgb(255, 50, 50, 50);
                }
            }
            else
            {
                this.serviceStatusLabel.BackColor = Color.FromArgb(255, 65, 65, 65);
                this.serviceStatusLabel.ForeColor = Color.FromArgb(255, 255, 255, 255);
            }
            tickCount++;
        }

        private void mysqlBtn_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.StateChange += new StateChangeEventHandler(this.conn_state_change);
            conn.InfoMessage += new MySqlInfoMessageEventHandler(this.conn_info_message);
            conn.ConnectionString = "Data Source=localhost;Database=celot_db;" + "User Id=root;Password=a98310" + ";charset=utf8";
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //conn.Close();


        }

        private void conn_info_message(object sender, MySqlInfoMessageEventArgs e)
        {
            Debug.WriteLine("[Info Message]");
            Debug.WriteLine("InfoMesage : {0}", e.errors);
            Debug.WriteLine("");
        }

        private void conn_state_change(object sender, StateChangeEventArgs e)
        {
            Debug.WriteLine("[Conn_state_change]");
            Debug.WriteLine("원래 상태 {0} , 현재 상태{1}", e.OriginalState, e.CurrentState);
            Debug.WriteLine("");
        }

        private void excelBtn_Click_1(object sender, EventArgs e)
        {
            switch (this.deviceBatchfileStatus)
            {
                case 0 :
                    this.fileDialog.FileName = "";
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = fileDialog.FileName;
                        string fileExt = Path.GetExtension(fileName);
                        if (!fileExt.Equals(".xlsx") && !fileExt.Equals(".xls"))
                        {
                            MessageBox.Show("EXCEL 포맷 파일만 가능합니다");
                            return;
                        }
                        this.deviceFileName = fileName;
                        this.deviceFileBox.Text = fileName;
                        this.deviceBatchfileStatus = 1;
                        this.setDeviceBatchBtnText();
                    }
                    break;
                case 1:
                    try
                    {
                        this.BatchDeviceInfo(this.deviceFileName);
                    }catch(Exception ee){
                        MessageBox.Show(String.Format("[ERROR] {0}\n[{1}]\n{2}","error occured in loading device file and parsing\nsee the log",ee.Message,ee.StackTrace));
                        this.deviceFileName = "";
                        this.deviceFileBox.Text = "";
                        this.deviceBatchfileStatus = 0;
                        this.setDeviceBatchBtnText();
                    }
                    break;
            }
        }


        private void BatchDeviceInfo(string fileName)
        {
            try
            {
                object missing = System.Reflection.Missing.Value;

                string strProvider = string.Empty;
                strProvider = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0\"";

                OleDbConnection excelConnection = new OleDbConnection(strProvider);
                excelConnection.Open();

                string strQuery = "SELECT * FROM [Sheet1$]";
                OleDbCommand dbCommand = new OleDbCommand(strQuery, excelConnection);
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(dbCommand);

                DataTable dTable = new DataTable();
                dataAdapter.Fill(dTable);

                List<CelotMClient.Model.Device> deviceList = new List<CelotMClient.Model.Device>();
                CelotMClient.Model.Device device;
                int rowIndex = 0;
                foreach (DataRow row in dTable.Rows)
                {
                    device = new CelotMClient.Model.Device();
                    device.Name = Convert.ToString(row[ExcelColumnIndex.NAME]);
                    device.SerialNo = Convert.ToInt32(row[ExcelColumnIndex.SERIAL_NUMBER]);
                    device.SecuCode = Convert.ToString(row[ExcelColumnIndex.SECURE_CODE]);
                    device.PhoneNumber = Convert.ToInt32(row[ExcelColumnIndex.PHONE_NUMBER]);
                    device.GroupName = Convert.ToString(row[ExcelColumnIndex.GROUP]);
                    device.RouterIp = Convert.ToString(row[ExcelColumnIndex.ROUTER_IP]);
                    device.Latitude = Convert.ToString(row[ExcelColumnIndex.LATITUDE]);
                    device.Longitude = Convert.ToString(row[ExcelColumnIndex.LONGITUDE]);
                    device.SmsSupport =
                        Convert.ToString(row[ExcelColumnIndex.SMS]).ToUpper().Equals("ON") ? (int)SmsSupport.SUPPORTED : (int)SmsSupport.UNSUPPORTED;
                    device.WifiSupport =
                         Convert.ToString(row[ExcelColumnIndex.WIFI]).ToUpper().Equals("ON") ? (int)WifiSupport.SUPPORTED : (int)WifiSupport.UNSUPPORTED;
                    device.BatterySupport =
                        Convert.ToString(row[ExcelColumnIndex.BATTERY]).ToUpper().Equals("ON") ? (int)BatterySupport.SUPPORTED : (int)BatterySupport.UNSUPPORTED;
                    device.VpnSupport =
                         Convert.ToString(row[ExcelColumnIndex.VPN]).ToUpper().Equals("ON") ? (int)VpnSupport.SUPPORTED : (int)VpnSupport.UNSUPPORTED;
                    device.Des = Convert.ToString(row[ExcelColumnIndex.DES]);
                    deviceList.Add(device);
                    rowIndex++;
                    Debug.WriteLine(String.Format(
                        "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} | {11} | {12} ",
                         device.Name,
                         device.SerialNo,
                         device.SecuCode,
                         device.PhoneNumber,
                         device.GroupName,
                         device.RouterIp,
                         device.Latitude,
                         device.Longitude,
                         device.SmsSupport,
                         device.WifiSupport,
                         device.BatterySupport,
                         device.VpnSupport,
                         device.Des
                        ));
                }
                dTable.Dispose();
                dataAdapter.Dispose();
                dbCommand.Dispose();

                excelConnection.Close();
                excelConnection.Dispose();

                if (deviceList.Count < 1)
                {
                    MessageBox.Show("디바이스 입력 정보가 존재하지 않습니다");
                    return;
                }
                Debug.WriteLine("디바이스 개수 : " + deviceList.Count);

                bool isError = false;
                StringBuilder strBuilder = new StringBuilder();

                //최신의 디바이스 리스트로 업데이트 
                ApplicationCache.Instance().LoadDevices();
                List<CelotMClient.Model.Device> tempRegisteredList = new List<CelotMClient.Model.Device>(ApplicationCache.Instance().Devices);
                foreach (CelotMClient.Model.Device d1 in deviceList)
                {
                    var query = tempRegisteredList.Where(x => x.PhoneNumber == d1.PhoneNumber || x.SerialNo == d1.SerialNo);
                    if (query.Count() > 0)
                    {
                        isError = true;
                        strBuilder.AppendLine(String.Format("Router Info overlapped :  Ip {0}, Phone {1}, Serial {2}", d1.RouterIp, d1.PhoneNumber, d1.SerialNo));
                    }

                    if (!CelotUtility.CheckValidIp(d1.RouterIp))
                    {
                        isError = true;
                        strBuilder.AppendLine(String.Format("wrong ip format  :  Ip {0}, Phone {1}, Serial {2}", d1.RouterIp, d1.PhoneNumber, d1.SerialNo));
                    }
                    tempRegisteredList.Add(d1);
                }

                if (isError)
                {
                    Logger.singleton().log(logLevel.Error, "DEVICE REGISTER ERROR", strBuilder.ToString());
                    MessageBox.Show("파일에  잘못된 디바이스 정보가 있습니다.등록을 취소합니다.\n자세한 사항은 로그파일을 확인해주세요");
                    this.deviceFileName = "";
                    this.deviceFileBox.Text = "";
                    this.deviceBatchfileStatus = 0;
                    this.setDeviceBatchBtnText();
                    return;
                }

                string insertBatchQuery = this.BuildInsertBatchQuery(deviceList);
                Debug.WriteLine(insertBatchQuery);

                CCloudDao cDao = new CCloudDao();
                cDao.NotifyDataBaseFinished += new NotifyDataBaseFinishedHandler(DeviceBatch_Finished);
                cDao.RawAffectedQuery(insertBatchQuery);
            }catch(Exception ee){
                throw ee;
            }
        }

        public string BuildInsertBatchQuery(List<CelotMClient.Model.Device> deviceList)
        {
            string query = "";
            query +=
                    @"INSERT INTO device ( 
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
                     VALUES";
            
            for (int i = 0; i < deviceList.Count; i++)
            {
                query += "(";
                query += "'" + deviceList[i].GroupName + "'" + ",";
                query += "'" + deviceList[i].Name + "'" + ",";
                query += deviceList[i].SerialNo + ",";
                query += "'" + deviceList[i].SecuCode + "'" + ",";
                query += deviceList[i].PhoneNumber + ",";
                query += "'" + deviceList[i].RouterIp + "'" + ",";
                query += deviceList[i].Latitude + ",";
                query += deviceList[i].Longitude + ",";
                query += "'" + deviceList[i].Des + "'" + ",";
                query += deviceList[i].SmsSupport + ",";
                query += deviceList[i].BatterySupport + ",";
                query += deviceList[i].WifiSupport + ",";
                query += deviceList[i].VpnSupport + ",";
                query += (int)((DateTime.Now.ToLocalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds); ;

                query += ")";
                if (i != deviceList.Count - 1){
                    query += " , ";
                }
            }
            return query;
        }

        private void DeviceBatch_Finished(object Sender, DataBaseFinishedEventArgs e)
        {
            bool result = e.Succeed;
            if (result == false)
            {
                MessageBox.Show(String.Format("ERROR::{0}", e.Message));
                return;
            }

            this.deviceFileName = "";
            this.deviceFileBox.Text = "";
            this.deviceBatchfileStatus = 0;
            this.setDeviceBatchBtnText();
            MessageBox.Show("디바이스 일괄 배치 생성 작업이 끝났습니다");
            if (!ServiceManager.Instance().ControlService(ServiceManager.CONTROL_SERVICE_RELOAD_DEVICE))
            {
                MessageBox.Show(ServiceManager.Instance().Message);
            }
            ApplicationCache.Instance().LoadDevices();
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string message = "";
            if (DatabaseManager.Instance().CheckConnection())
            {
                message = "데이타 베이스에서 연결할 수 있습니다";
            }
            else
            {
                message = "데이타 베이스에 연결할 수 없습니다.";
            }
            MessageBox.Show(message);
        }

        private void serviceBtn_Click(object sender, EventArgs e)
        {
            if (!CelotUtility.IsAdministratorRun())
            {
                MessageBox.Show("관리자 권한이 필요합니다");
                return;
            }

            CelotServiceStatus status = ServiceManager.Instance().ServiceStatus;
            switch (status)
            {
                case CelotServiceStatus.NotInstalled:
                    InstallService();
                    break;
                case CelotServiceStatus.Running:
                    StopService();
                    break;
                case CelotServiceStatus.Stopped:
                    StartService();
                    break;
            }
            InitControl();
            
        }

        private void StartService()
        {
            if (!CelotUtility.IsAdministratorRun())
            {
                MessageBox.Show("관리자 권한이 필요합니다");
                return;
            }

            if (MessageBox.Show("서비스를 시작하시겠습니까", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (ServiceManager.Instance().StartService())
                {
                    InitControl();
                }
                MessageBox.Show(ServiceManager.Instance().Message);
            }
        }

        private void StopService()
        {
            if (!CelotUtility.IsAdministratorRun())
            {
                MessageBox.Show("관리자 권한이 필요합니다");
                return;
            }

            if (MessageBox.Show("서비스를 중지하시겠습니까", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (ServiceManager.Instance().StopService())
                {
                    InitControl();
                }
                MessageBox.Show(ServiceManager.Instance().Message);
            }
        }

        private void InstallService()
        {
            if (!CelotUtility.IsAdministratorRun())
            {
                MessageBox.Show("관리자 권한이 필요합니다");
                return;
            }

            this.fileDialog.FileName = "";
            if (this.fileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = this.fileDialog.FileName;
                if (!Path.GetExtension(filePath).Equals(".exe") && !Path.GetExtension(filePath).Equals(".EXE"))
                {
                    MessageBox.Show("옳바른 서비스 실행파일이 아닙니다.");
                    return;
                }
                ApplicationConfig config = ApplicationConfig.Instance();
                ServiceManager.Instance().InstallService();
                MessageBox.Show(ServiceManager.Instance().Message);
            }
        }

        private void excelBtn_Click(object sender, EventArgs e)
        {
            this.fileDialog.FileName = "";
   
        }

        private void dbChangBtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.dbHostBox.Text))
            {
                MessageBox.Show("DB host를 입력해주세요");
                this.dbHostBox.Focus();
                return;
            }

            if (String.IsNullOrEmpty(this.dbNameBox.Text))
            {
                MessageBox.Show("DB 명을 입력해주세요");
                this.dbNameBox.Focus();
                return;
            }

            if (String.IsNullOrEmpty(this.dbIdBox.Text))
            {
                MessageBox.Show("DB ID를 입력해주세요");
                this.dbIdBox.Focus();
                return;
            }

            if (String.IsNullOrEmpty(this.dbPassBox.Text))
            {
                MessageBox.Show("DB Passwrod 를 입력해주세요");
                this.dbPassBox.Focus();
                return;
            }

            if (String.IsNullOrEmpty(this.dbCharsetBox.Text))
            {
                MessageBox.Show("DB 캐릭터셋을 입력해주세요");
                this.dbCharsetBox.Focus();
                return;
            }

            ApplicationConfig config = ApplicationConfig.Instance();
            config.DatabaseHost = this.dbHostBox.Text;
            config.DatabaseName = this.dbNameBox.Text;
            config.DatabaseId = this.dbIdBox.Text;
            config.DatabasePassword = this.dbPassBox.Text;
            config.DatabaseCharset = this.dbCharsetBox.Text;
        }

        private void updatePeriodBtn_Click_1(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.updatePeriodBox.Text))
            {
                return;
            }

            int updatePeriod = Convert.ToInt32(this.updatePeriodBox.Text);
            ApplicationConfig.Instance().UpdatePeriod = updatePeriod;
            ApplicationCache.Instance().SetTimerInterval(updatePeriod);
            this.updatePeriodLabel.Text = this.updatePeriodBox.Text;
            // this.updatePeriodBox.Text = "";
        }

        private void lowBatteryBtn_Click_1(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.lowBatteryBox.Text))
            {
                return;
            }

            int lowBatteryLimit = Convert.ToInt32(this.lowBatteryBox.Text);
            ApplicationConfig.Instance().LowBatteryLimit = lowBatteryLimit;
            ApplicationCache.Instance().Init();
            this.lowBatteryLabel.Text = this.lowBatteryBox.Text;
            //this.lowBatteryBox.Text = "";
        }

        private void menuEditBtn_Click_1(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Point p = this.PointToScreen(button.Location);
            MenuEditForm editForm = new MenuEditForm();
            editForm.Location = this.PointToScreen(button.Location);
            editForm.ShowDialog();
        }

        private void setDeviceBatchBtnText()
        {
            if (this.deviceBatchfileStatus == 0)
            {
                this.excelBtn.Text = "File Browse";
            }
            else
            {
                this.excelBtn.Text = "Start Batch";
            }
        }

        private void deviceFileBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.deviceFileBox.Text))
            {
                this.deviceBatchfileStatus = 0;
                this.setDeviceBatchBtnText();
            }
        }

        private void coordBtn_Click(object sender, EventArgs e)
        {
            CoordForm coordForm = new CoordForm();
            if (coordForm.ShowDialog() == DialogResult.OK)
            {
                this.InitControl();
            }
        }

        private void portBtn_Click(object sender, EventArgs e)
        {
            int portInt;
            bool re = int.TryParse(portBox.Text, out portInt);
            if (!re || portInt < 1 || portInt> 65535 )
            {
                MessageBox.Show("포트 번호는 1 ~ 65525 사이의 숫자여야 합니다.");
                return;
            }

            if (ServiceManager.Instance().QueryServiceStatus() == CelotServiceStatus.NotInstalled)
            {
                MessageBox.Show("서비스가 설치되지 않는 상태입니다");
                return;
            }

            if (MessageBox.Show("서버가 지정한 포트로 재 시작합니다", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ApplicationConfig.Instance().ServicePort = portInt;
                CelotServiceStatus status = ServiceManager.Instance().QueryServiceStatus();
                ServiceManager.Instance().StopService();
                ServiceManager.Instance().StartService();
                MessageBox.Show(String.Format("Service restarted at port [{0}]", ApplicationConfig.Instance().ServicePort));
                InitControl();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!CelotUtility.IsAdministratorRun())
            {
                MessageBox.Show("관리자 권한이 필요합니다");
                return;
            }

            int protocoIndex = protocolCombo.SelectedIndex;
            if (protocoIndex == 1 && ApplicationConfig.Instance().SNMPSupport == false)
            {
                MessageBox.Show("현재 SNMP 프로토콜은 지원하지 않습니다");
            }
        }

        private void nmsLimitBtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(nmsLimitBox.Text.Trim()))
            {
                int nmsLImit = 0;
                bool result = int.TryParse(nmsLimitBox.Text.Trim(), out nmsLImit);
                if (!result)
                {
                    MessageBox.Show("분 단위 숫자만 입력이 가능합니다");
                    return;
                }
                ApplicationConfig.Instance().NmsReportLimit = nmsLImit;
            }
        }
    }
}
