    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using CelotMClient.Manager;
using CelotMClient.CustomControll;
using CelotMClient.Worker;
using CelotMClient.Model;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CelotMClient.NMSStructure;
using CelotMClient.Model.NMS;
using CelotMClient.Util;
using System.IO;

namespace CelotMClient
{
    public partial class MainForm : Form
    {
        public bool isMenuExtended = true;
        public int extendRate = 40;
        public int splitterDistance = 0;
        public bool menuHideEanbler = true;
        public int formWidth = 0;

        public UserControl curUserControll;
        public MainForm()
        {
            Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
            splashthread.IsBackground = true;
            splashthread.Start();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopLevel = true;
            this.TopMost = true;
            CelotApplication.Instance().ViewTransferEnable = true;
            try
            {
                SplashScreen.UdpateStatusTextWithStatus("Initializing Application Config", TypeOfMessage.Success);
                Thread.Sleep(100);

                IniUtil.Instance().Init();
                ApplicationConfig.Instance().init();

                Thread.Sleep(100);
                SplashScreen.UdpateStatusTextWithStatus("Checking Database", TypeOfMessage.Success);

                if (!DatabaseManager.Instance().CheckConnection())
                {
                    SplashScreen.CloseSplashScreen();
                    Thread.Sleep(300);
                    MessageBox.Show(String.Format("[ERROR] Database connection failed\n{0}", DatabaseManager.Instance().Message));
                    Application.Exit();
                }
                else
                {
                    SplashScreen.UdpateStatusTextWithStatus("Initializing Application data", TypeOfMessage.Success);
                    Logger logger = Logger.singleton(null, 1024 * 100, 6);

                    ServiceManager.Instance().Init();

                    CelotApplication.Instance().MainForm = this;

                    ApplicationCache.Instance().Init();
                    //데이타를 수신받기 위한 이벤트 등록 
                    ApplicationCache.Instance().NMSDataNotify += new NMSDataNotiHandler(this.Main_NMSDataNotify);

                    ApplicationCache.Instance().MainForm = this;

                    Thread.Sleep(100);
                    SplashScreen.UdpateStatusTextWithStatus("Application starting", TypeOfMessage.Success);

                    Thread.Sleep(100);
                    SplashScreen.CloseSplashScreen();
                    
                    CustomView.DashBoard2 dash = new CustomView.DashBoard2(this);
                    dash.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);
                    dash.Dock = DockStyle.Fill;
                    splitContainer1.Panel2.Controls.Add(dash);
                    curUserControll = dash;
                    timeTimer.Start();
                    
                    this.changeMenu();
                    if (this.menuHideEanbler)
                    {
                        this.menuExtender.Visible = true;
                    }
                    else
                    {
                        this.menuExtender.Visible = false;
                    }
                    this.formWidth = this.Width;
                    splitterDistance = this.splitContainer1.SplitterDistance;
                    this.TopMost = false;
                }
            }
            catch (Exception ee)
            {
                Debug.WriteLine(ee.Message);
                Debug.WriteLine(ee.StackTrace);
            }
            finally
            {
                SplashScreen.CloseSplashScreen();
            }
        }
       

        private void dashButton_Click(object sender, EventArgs e)
        {
            if (!this.ViewTransferEanble()) return;
            splitContainer1.Panel2.Controls.Clear();
            DisposeCurUserControl();
            CustomView.DashBoard2 dash = new CustomView.DashBoard2(this);
            dash.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);
            dash.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(dash);
            curUserControll = dash;
        }

        private bool ViewTransferEanble()
        {
            if (!CelotApplication.Instance().ViewTransferEnable)
            {
                MessageBox.Show(String.Format("현재 {0} 작업이 진행중입니다.", CelotApplication.Instance().JobType), "작업 진행중");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void deviceButton_Click(object sender, EventArgs e)
        {
            if (!this.ViewTransferEanble()) return;
            splitContainer1.Panel2.Controls.Clear();
            DisposeCurUserControl();
            CustomView.DeviceView deviceView = new CustomView.DeviceView();
            deviceView.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);
            deviceView.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(deviceView);
            curUserControll = deviceView;
        }

        private void settingButton_Click(object sender, EventArgs e)
        {
            if (!this.ViewTransferEanble()) return;
            splitContainer1.Panel2.Controls.Clear();
            DisposeCurUserControl();
            CustomView.SettingsView settingsView = new CustomView.SettingsView();
            settingsView.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);
            settingsView.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(settingsView);
            curUserControll = settingsView;
        }

        private void smsButton_Click(object sender, EventArgs e)
        {
            if (!this.ViewTransferEanble()) return;
            splitContainer1.Panel2.Controls.Clear();
            DisposeCurUserControl();
            CustomView.SmsView smsView = new CustomView.SmsView();
            smsView.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);
            smsView.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(smsView);
            curUserControll = smsView;
        }

        private void appButton_Click(object sender, EventArgs e)
        {
            if (!this.ViewTransferEanble()) return;
            splitContainer1.Panel2.Controls.Clear();
            DisposeCurUserControl();
            CustomView.ApplicationView aView = new CustomView.ApplicationView();
            aView.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);
            aView.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(aView);
            curUserControll = aView;
        }

        private void alertButton_Click(object sender, EventArgs e)
        {
            if (!this.ViewTransferEanble()) return;
            splitContainer1.Panel2.Controls.Clear();
            DisposeCurUserControl();
            CustomView.AlertView alertView = new CustomView.AlertView();
            alertView.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);
            alertView.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(alertView);
            curUserControll = alertView;
        }

        private void DisposeCurUserControl()
        {
            if (this.curUserControll != null)
            {
                this.curUserControll.Dispose();
            }
        }

        public void MoveDeviceView(string routerName, int serialNo, string routerIp)
        {
            this.deviceButton.PerformClick();
        }

        public void MoveApplicatioinView()
        {
            this.appButton.PerformClick();
        }

        StringBuilder stringBuilder = new StringBuilder(); 
        private void Main_NMSDataNotify(object sender, NMSDataNotifyEventArgs args)
        {
            this.Invoke(new Action(delegate()
            {
                NMSDataWrapper dataWapper = args.Data;
                stringBuilder.Clear();
                //총 수량, 정상, 장애 
                this.ShowDeiviceSummary(ApplicationCache.Instance().Devices.Count, 
                    ApplicationCache.Instance().Devices.Count - dataWapper.RouterErrCnt, dataWapper.RouterErrCnt);
                
                //Recent Alert
                NMSReportCommand errCommand = dataWapper.GetRecentAlertNMSReportCommand();
                if (errCommand != null)
                {
                    stringBuilder.AppendFormat(
                        "1.Name : {0}\n2.Version : {1}\n3.Time : {2}\n4.Type : {3}\n5.Error Detai : {4}",
                        errCommand.Device.Name,
                        errCommand.nms_reprot_t == null ? "" : errCommand.nms_reprot_t.data.hw_version,
                        errCommand.nms_reprot_t == null ? "" : CelotUtility.ChangeStampStringToLocalFormat(errCommand.nms_reprot_t.data.current_time),
                        errCommand.AlertList[0].AlertType.Parse(),
                        errCommand.GetAlertMessages().Trim().Substring(0).Replace(",", "\n-")
                        );
                }
                this.alertLabel.Text = stringBuilder.ToString(); ;
                stringBuilder.Clear();
                
                /*
                 * 대쉬보드가 열려 있으면 대쉬보드의 구글맵의 Layer 데이타를 전송
                 * 현재 대쉬보드내에서 이를 처리함으로 주석처리 
                 * 즉시적인 데이타 갱신이 필요하면 대쉬보드의 해당 구문을 주석처리하고 아래의 구문을 사용
                if (this.curUserControll is CustomView.DashBoard2)
                {
                   CustomView.DashBoard2 dash  =  curUserControll as CustomView.DashBoard2;
                   dash.DispatchEventData(
                       ApplicationCache.Instance().Devices.Count, 
                       ApplicationCache.Instance().Devices.Count - dataWapper.RouterErrCnt,
                       dataWapper.RouterErrCnt,
                       result.Replace("\n","<br/>")
                       );
                }
                 * */
                dataWapper = null;
            }));

        }

        public void changeMenu()
        {
            bool[] menuSettings = ApplicationConfig.Instance().MenuSettings;
            this.dashButton.Visible = menuSettings[MenuIndex.Dashboard];
            this.deviceButton.Visible = menuSettings[MenuIndex.Device];
            this.alertButton.Visible = menuSettings[MenuIndex.Alert];
            this.appButton.Visible = menuSettings[MenuIndex.Application];
            this.smsButton.Visible = menuSettings[MenuIndex.Sms];
            menuTableLayoutPanel.PerformLayout();
        }

        public void ShowDeiviceSummary(int deviceCount, int normalCount, int problemCount)
        {
            this.totalDeviceCount.Text = deviceCount.ToString();
            this.normalDeviceCount.Text = normalCount.ToString();
            this.proDeviceCount.Text = problemCount.ToString();
        }

        private void timeTimer_Tick(object sender, EventArgs e)
        {
            this.timeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.singleton().shutdown();
        }

        private void menuExtender_Click(object sender, EventArgs e)
        {
            if (menuHideEanbler)
            {
                this.SuspendLayout();
                if (isMenuExtended)
                {
                    this.splitContainer1.SplitterDistance = 30;
                    this.menuExtender.Text = ">";
                    this.isMenuExtended = false;
                    this.tableLayoutPanel2.Visible = false;
                    this.alertPanel.Visible = false;
                    this.reservcePanel.Visible = false;
                }
                else
                {
                    this.splitContainer1.SplitterDistance = this.splitterDistance;
                    this.menuExtender.Text = "<";
                    this.isMenuExtended = true;
                    this.tableLayoutPanel2.Visible = true;
                    this.alertPanel.Visible = true;
                    this.reservcePanel.Visible = true;
                }
                this.ResumeLayout();
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            //if (this.menuHideEanbler)
            //{
            //    if (this.Width < 900)
            //    {
            //        if (this.isMenuExtended)
            //        {
            //            this.menuExtender.PerformClick();
            //        }
            //    }
            //}
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.ViewTransferEanble())
            {
                e.Cancel = true;
            }
        }
    }
}
