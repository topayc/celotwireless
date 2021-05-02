using CelotMClient.Api;
using CelotMClient.Manager;
using CelotMClient.Setup;
using CelotMClient.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.EnterpriseServices.Internal;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.CustomForm
{
    public partial class SetupForm : Form
    {
        public string serviceName;
        public string serviceDisplayName;
        public string serviceFile;
        public int servicePort;
        public int maxSessionCount;
        public int protocol;
        public string mySqlSeviceName;
        public string dbId;
        public string dbPass;
        public int remotePort;
        public int concurrentDownloadCount;
        public string logFile;
        public int lowBatteryLimit;
        public int updatePeriod;
        public double lat;
        public double lng;
        public string des;
        public string dbHost;

        public bool beingInstall = false;
        public bool isBeingInstall = false;
        public bool isDbConnected = false;

      

        private Setup1 setup1;
        private Setup2 setup2;

        private bool installSuccess = false;

        private int curPage;
        public SetupForm()
        {
            serviceName = "C-Cloud";
            serviceDisplayName = "C-Cloud";
            serviceFile = "";
            servicePort = 2000;
            maxSessionCount = 100;
            protocol = 100;
            dbId = "root";
            dbPass = "";
            mySqlSeviceName ="";
            remotePort = 1011;
            concurrentDownloadCount = 30;
            logFile = Path.Combine(Application.StartupPath, "celot.log");
            lowBatteryLimit = 1000;
            updatePeriod = 4000;
            lat = 37.3946003;
            lng = 126.9625997;
            des = "[C-CLOUD]\n버젼 : v1.1.0\n날짜 : 2016년 1월 1일\nCopyright c 2010-2016 celotwiress All right reserved\nwww.celotwireless.com";
            dbHost = "127.0.0.1";
            InitializeComponent();
        }

        private void SetupForm_Load(object sender, EventArgs e)
        {
            if (!CelotUtility.IsAdministratorRun())
            {
                MessageBox.Show("설치과정을 진행하기 위해서는 관리자 권한으로 실행해야 합니다.\n다시 시도해 주세요");
            }

            setup1 = new Setup1(this);
            setup2 = new Setup2(this);

            panel2.Controls.Clear();
            panel2.Controls.Add(setup1);

            curPage = 1;
            isBeingInstall = false;
            setControl();
        }

        private void setControl()
        {
            button4.Enabled = !isBeingInstall;
            button3.Enabled = !isBeingInstall;
            if (curPage == 1)
            {
                button3.Text = "다음";
                prevBtn.Visible = false;
                button3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                button3.Text = "시작";
                if (installSuccess)
                {
                    prevBtn.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = true;
                }
                else
                {
                    prevBtn.Enabled = true;
                    prevBtn.Visible = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (beingInstall)
            {
                MessageBox.Show("설치작업이 진행중입니다");
                return;
            }
            if (installSuccess)
            {
                if (restartCheck.Checked)
                {
                    Application.Restart();
                }
                else
                {
                    this.Close();
                    Application.Exit();
                }
            }
            else
            {
                if (MessageBox.Show("종료하시겠습니까", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Close();
                    Application.Exit();
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (curPage == 1)
            {
                if (CheckFields())
                {
                    panel2.Controls.Clear();
                    panel2.Controls.Add(setup2);
                    isBeingInstall = false;
                    curPage = 2;
                    setControl();

                }
            }
            else
            {
                if (beingInstall)
                {
                    MessageBox.Show("설치작업이 진행중입니다");
                    return;
                }
                beingInstall = true;
                setup2.progressTextBox.Text = "";
                setup2.resultBox.Text = "";

                isBeingInstall = true;
                setControl();
                startInstall();
            }
        }

        private void startInstall()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(db_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(db_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(db_RunWorkerCompleted);
            worker.RunWorkerAsync();
        }

        private void db_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            beingInstall = false;
            if (errStrBuilder.Length > 1)
            {
                ApplicationConfig.Instance().Install = 0;
                installSuccess = false;
                setControl();
                setup2.progressTextBox.AppendText("> 설치 실패");
                setup2.resultBox.Text = errStrBuilder.ToString();
            }
            else
            {
                ApplicationConfig.Instance().Install = 1;
                installSuccess = true;
                setup2.progressTextBox.AppendText("> 설치 완료");
                isBeingInstall = false;
                setControl();
                prevBtn.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void db_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        private StringBuilder errStrBuilder = new StringBuilder();
        private void db_DoWork(object sender, DoWorkEventArgs e)
        {
            errStrBuilder.Clear();

            //mysql connector 설치 
            invokeMessage("> MySql Connector 설치중\n");
            Thread.Sleep(100);
            Publish publish = null;
            try
            {
                publish = new Publish();
                publish.GacInstall(Path.Combine(Application.StartupPath, "MySql.Data.dll"));
            }
            catch (Exception ee)
            {
                errStrBuilder.AppendLine(String.Format("> {0} : {1}", "어셈블리 설치 에러", ee.Message));
                Debug.WriteLine("어셈블리 설치 에러 " + ee.Message);
                Debug.WriteLine(ee.Message);
                return;
            }
            invokeMessage("> MySql Connector를 설치했습니다\n");
            Thread.Sleep(200);

            try
            {
                //DB Connection check
                if (!isDbConnected)
                {
                    invokeMessage(String.Format("> 데이타 베이스[{0}][{1}][{2}]에 연결중\n", dbHost, dbId, dbPass));
                    Thread.Sleep(100);
                    MySqlConnection con5 = null;
                    string connectionString = String.Format(
                        "Data Source={0};User Id={1};Password={2};charset={3}",
                        dbHost, dbId, dbPass, "utf8");
                    con5 = new MySqlConnection();
                    con5.ConnectionString = connectionString;
                    con5.Open();
                    invokeMessage(String.Format("> 데이타 베이스[{0}][{1}][{2}]에 연결할 수 있습니다\n", dbHost, dbId, dbPass));
                }
            }
            catch (Exception e1)
            {
                errStrBuilder.AppendLine(String.Format(String.Format("> 데이타 베이스[{0}][{1}][{2}]에 연결할 수 없습니다\n", dbHost, dbId, dbPass)));
                return;
            }

            //write config 
            invokeMessage(String.Format("> 설정 파일(config.ini)을 작성중\n"));
            Thread.Sleep(100);

            try
            {
                //SERVICE section
                ApplicationConfig.Instance();
                ApplicationConfig.Instance().ServiceName = this.serviceName;
                ApplicationConfig.Instance().ServiceDisplayName = this.serviceDisplayName;
                ApplicationConfig.Instance().ServiceFilePath = this.serviceFile;
                ApplicationConfig.Instance().MaxSessionCount = this.maxSessionCount;
                ApplicationConfig.Instance().ServicePort = this.servicePort;

                //PROTOCOL section
                ApplicationConfig.Instance().UsingProtocol = this.protocol;
                ApplicationConfig.Instance().NMSSupport = true;
                ApplicationConfig.Instance().SNMPSupport = false;

                //CLIENT section
                ApplicationConfig.Instance().LogFilename = this.logFile;
                ApplicationConfig.Instance().RemotePort = this.remotePort;
                ApplicationConfig.Instance().UpdatePeriod = this.updatePeriod;
                ApplicationConfig.Instance().LowBatteryLimit = this.lowBatteryLimit;

                ApplicationConfig.Instance().ConcurrentDownloadCount = this.concurrentDownloadCount;
                ApplicationConfig.Instance().Latitude = this.lat;
                ApplicationConfig.Instance().Longitude = this.lng;
                //ApplicationConfig.Instance().ProtocolDes = des;

                //DATABASE section
                ApplicationConfig.Instance().DatabaseHost = dbHost;
                ApplicationConfig.Instance().DatabaseName = "celot_db";
                ApplicationConfig.Instance().DatabaseId = dbId;
                ApplicationConfig.Instance().DatabasePassword = dbPass;
                ApplicationConfig.Instance().DatabaseCharset = "utf8";

            }
            catch (Exception eee)
            {
                errStrBuilder.AppendLine(String.Format("> 설정 파일(config.ini)을 작성 중에 에러가 발생했습니다 : {0}\n", eee.Message));
                Debug.WriteLine(eee.StackTrace);
                return;
            }
            invokeMessage(String.Format("> 설정 파일(config.ini)을 작성했습니다\n"));
            Thread.Sleep(100);

            //DB 스키마 생성 
            Thread.Sleep(100);
            MySqlConnection con = null;
            MySqlCommand command = null;
            try
            {
                //Create Database and table
                invokeMessage(String.Format("> 데이타 베이스 Schema[celot_db]와 테이블을  생성중\n"));
                Thread.Sleep(200);

                string connectionString = String.Format(
                    "Data Source={0};User Id={1};Password={2}",
                    dbHost, dbId, dbPass);
                con = new MySqlConnection();
                string script = File.ReadAllText("new_celot_schema.sql");
                con.ConnectionString = connectionString;
                con.Open();

                command = new MySqlCommand(script, con);
                command.ExecuteNonQuery();

                string adminGroupCreateScript = @"INSERT INTO celot_db.admin_group(Code, Name,Des) VALUES";
                adminGroupCreateScript += String.Format(@"('{0}','{1}','{2}'),", "SA", "Super Admin", "Super Admin");
                adminGroupCreateScript += String.Format(@"('{0}','{1}','{2}'),", "MA", "Manager", "Manager");
                adminGroupCreateScript += String.Format(@"('{0}','{1}','{2}')", "CO", "Common", "Common");
                command = new MySqlCommand(adminGroupCreateScript, con);
                command.ExecuteNonQuery();

                invokeMessage(String.Format("> 데이타 베이스 Schema[celot_db]와 테이블을  생성했습니다\n"));
                Thread.Sleep(100);
            }
            catch (Exception ex2)
            {
                errStrBuilder.AppendLine(String.Format("> Schema 생성 에러 : {0}\n", ex2.Message));
                return;
            }
            finally
            {
                if (con != null) con.Close();
            }
            Thread.Sleep(100);

            invokeMessage(String.Format("> C-Cloud 서비스를 설치중\n"));
            Thread.Sleep(100);
            if (!ServiceManager.Instance().InstallService(
                this.serviceName, 
                this.serviceDisplayName, 
                this.serviceFile,
                this.mySqlSeviceName))
            {
                errStrBuilder.AppendLine(String.Format("> {0} : {1}", "C-Cloud 서비스 설치 에러", ServiceManager.Instance().Message));
                return;
            }

            CelotWinApi.EnableWow64DisableWow64FsRedirection();
            try
            {
                if (File.Exists(Path.Combine(Environment.SystemDirectory, "config.ini")))
                {
                    File.Delete(Path.Combine(Environment.SystemDirectory, "config.ini"));
                }

                if (File.Exists(Path.Combine(Environment.SystemDirectory, "CELOT_SERVICE_LOG.log")))
                {
                    File.Delete(Path.Combine(Environment.SystemDirectory, "CELOT_SERVICE_LOG.log"));
                }
            }
            catch (Exception ee)
            {
                errStrBuilder.AppendLine(String.Format("> 기존의 onfig.ini 제거 실패 {0}\n", ee.Message));
                return;
            }

            try
            {
                File.Copy(IniUtil.INI_FILENAME, Path.Combine(Environment.SystemDirectory, "config.ini"), true);

            }
            catch (Exception ee)
            {
                errStrBuilder.AppendLine(String.Format("> config.ini 파일 생성 실패 {0}\n", ee.Message));
                return;
            }

            CelotWinApi.DisableWow64DisableWow64FsRedirection();

            invokeMessage(String.Format("> C-Cloud 서비스를 설치했습니다\n"));
            Thread.Sleep(100);

            invokeMessage(String.Format("> C-Cloud 서비스를 시작하는 중\n"));
            Thread.Sleep(100);

            try
            {
                if (!ServiceManager.Instance().StartService())
                {
                    errStrBuilder.AppendLine(String.Format("> {0} : {1}", "C-Cloud 서비스 시작 에러", ServiceManager.Instance().Message));
                    return;
                }
                invokeMessage(String.Format("> C-Cloud 서비스를 시작했습니다\n"));
                Thread.Sleep(100);
                ApplicationConfig.Instance().Install = 1;
            }
            catch (Exception ex)
            {
                errStrBuilder.AppendLine(String.Format("> {0} : {1}", " C-Cloud 서비스 설치 에러", ex.Message));
                Debug.WriteLine("서비스 설치 에러");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

        }

        private void invokeMessage(string message)
        {
            setup2.progressTextBox.Invoke(
               new Action(delegate()
               {
                   setup2.progressTextBox.AppendText(message);
               }
           ));
        }

        private bool CheckFields()
        {
            this.serviceName = setup1.serviceNameBox.Text;
            if (string.IsNullOrEmpty(this.serviceName))
            {
                MessageBox.Show("서비스 이름을 입력해주세요");
                setup1.serviceNameBox.Focus();
                return false;
            }

            this.serviceDisplayName = setup1.serviceDiaplayBox.Text;
            if (string.IsNullOrEmpty(this.serviceDisplayName))
            {
                MessageBox.Show("서비스 표시 이름을 입력해주세요");
                setup1.serviceDiaplayBox.Focus();
                return false;
            }

            this.serviceFile = setup1.serviceFieBox.Text;
            if (string.IsNullOrEmpty(this.serviceFile))
            {
                MessageBox.Show("설치하려는 서비스 파일을 선택해주세요");
                setup1.serviceFieBox.Focus();
                return false;
            }

            if (!Path.GetExtension(this.serviceFile).Equals(".exe") && !Path.GetExtension(this.serviceFile).Equals(".EXE"))
            {
                MessageBox.Show("옳바른 서비스 실행파일이 아닙니다.");
                setup1.serviceFieBox.Focus();
                return false;
            }

            bool r1 = int.TryParse(setup1.servicePortBox.Text, out this.servicePort);
            if (!r1)
            {
                MessageBox.Show("포트번호는 숫자로 입력해주세요");
                setup1.servicePortBox.Focus();
                return false;
            }

            bool r2 = int.TryParse(setup1.sessionCountBox.Text, out this.maxSessionCount);
            if (!r2)
            {
                MessageBox.Show(" 숫자로 입력해주세요");
                setup1.sessionCountBox.Focus();
                return false;
            }
            
            this.protocol = setup1.protocolCombo.SelectedIndex == 0 ? 100 : 101;
           
            if (setup1.isDirectInputMySqlName) {
                this.mySqlSeviceName = setup1.mySqlNameBox.Text.Trim();    
            }else {
                this.mySqlSeviceName = setup1.mySqlNameCombo.SelectedValue.ToString().Trim();  
            }
            if (String.IsNullOrEmpty(this.mySqlSeviceName) || String.IsNullOrWhiteSpace(this.mySqlSeviceName))
            {
                MessageBox.Show("MySQL 데이타 베이스 서비스 이름을  입력해주세요");
                setup1.mySqlNameBox.Focus();
                return false;
            }
            this.dbHost = setup1.dbHostBox.Text.Trim();
            if (string.IsNullOrEmpty(this.dbHost))
            {
                MessageBox.Show("데이타 베이스 host 주소를 입력해주세요");
                setup1.dbHostBox.Focus();
                return false;
            }
            
            this.dbId = setup1.dbIdBox.Text.Trim();
            if (string.IsNullOrEmpty(this.dbId))
            {
                MessageBox.Show("데이타 베이스 계정 id 를 입력해주세요");
                setup1.dbIdBox.Focus();
                return false;
            }

            this.dbPass = setup1.dbPassBox.Text.Trim();
            if (string.IsNullOrEmpty(this.dbPass))
            {
                MessageBox.Show("데이타 베이스 계정 비밀번호 를 입력해주세요");
                setup1.dbPassBox.Focus();
                return false;
            }

            bool r3 = int.TryParse(setup1.remotePortBox.Text, out this.remotePort);
            if (!r3)
            {
                MessageBox.Show("포트번호는 숫자로 입력해주세요");
                setup1.remotePortBox.Focus();
                return false;
            }

            bool r4 = int.TryParse(setup1.downloadCountBox.Text, out this.concurrentDownloadCount);
            if (!r4)
            {
                MessageBox.Show("숫자로 입력해주세요");
                setup1.downloadCountBox.Focus();
                return false;
            }

            this.logFile = setup1.logFileBox.Text;
            if (string.IsNullOrEmpty(this.logFile))
            {
                MessageBox.Show("로그파일명을 입력해주세요");
                setup1.logFileBox.Focus();
                return false;
            }

            bool r5 = int.TryParse(setup1.lowBatteryLimitBox.Text, out this.lowBatteryLimit);
            if (!r5)
            {
                MessageBox.Show("숫자로 입력해주세요");
                setup1.lowBatteryLimitBox.Focus();
                return false;
            }

            bool r6 = int.TryParse(setup1.updatePeriodBox.Text, out this.updatePeriod);
            if (!r6)
            {
                MessageBox.Show("숫자로 입력해주세요");
                setup1.updatePeriodBox.Focus();
                return false;
            }

            bool r7 = double.TryParse(setup1.latBox.Text, out this.lat);
            if (!r7)
            {
                MessageBox.Show("숫자로 입력해주세요");
                setup1.latBox.Focus();
                return false;
            }

            bool r8 = double.TryParse(setup1.lngBox.Text, out this.lng);
            if (!r8)
            {
                MessageBox.Show("숫자로 입력해주세요");
                setup1.lngBox.Focus();
                return false;
            }

            this.des = setup1.desBox.Text;
            if (string.IsNullOrEmpty(this.des))
            {
                MessageBox.Show("설명을 입력해주세요");
                setup1.desBox.Focus();
                return false;
            }

            return true;
        }

        private void prevBtn_Click(object sender, EventArgs e)
        {
            if (curPage == 2)
            {
                if (beingInstall)
                {
                    MessageBox.Show("설치작업이 진행중입니다");
                    return;
                }
            }
            panel2.Controls.Clear();
            panel2.Controls.Add(setup1);
            curPage = 1;
            setControl();
        }
    }
}
