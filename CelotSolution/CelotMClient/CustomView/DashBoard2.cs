using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CelotMClient.Model;
using System.Security.Policy;
using CelotMClient.CustomControll;
using CelotMClient.CustomForm;
using CelotMClient.Worker;
using System.Threading;
using MySql.Data.MySqlClient;
using CelotMClient.Manager;
using System.Diagnostics;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using CelotMClient.Model.NMS;
using CelotMClient.NMSStructure;
using CelotMClient.Util;

namespace CelotMClient.CustomView
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class DashBoard2 : UserControl
    {
        public int select_map = 1;
        public MainForm parent;

        public List<DeviceReportCommand> deviceReportCommandList;
        public List<NameValuePair> technologyList;
        public List<NameValuePair> rssiLevelList;
        public bool isRouterMouseOver = false;
        public bool isMapExtend = false;
        public float chartRowHeight = 0;
        public NMSAlertManager alertManager = new NMSAlertManager();

        //구글맵 위의 정보 레이어 표시 여부
        public bool layerEnableOnMap = true;

        public DashBoard2(MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void DashBoard2_Load(object sender, EventArgs e)
        {
            this.initControl();
            this.chartRowHeight = this.tableLayoutPanel1.RowStyles[3].Height;
        }

        private void initControl()
        {
            this.webBrowser1.ScriptErrorsSuppressed = false;
            //this.webBrowser1.AllowWebBrowserDrop = false;
            //this.webBrowser1.ScrollBarsEnabled = false;
            //this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            //this.webBrowser1.WebBrowserShortcutsEnabled = false;
            this.webBrowser1.ObjectForScripting = this;
            
            if (CelotUtility.IsConnectedToInternet())
            {
                this.webBrowser1.DocumentText = Properties.Resources.celot_map;
            }
            else
            {
                this.webBrowser1.DocumentText = Properties.Resources.no_map_html;
            }
        }

        public void CallDeviceConfig(int serialNo)
        {
            NMSReportCommand nmsReportCommand = ApplicationCache.Instance().CurNMSDataWrapper.Find(serialNo);
            if (nmsReportCommand == null)
            {
                MessageBox.Show("선택하신 디바이스가 존재하지 않습니다");
                return;
            }
            RouterCommand routerCommand = new RouterCommand(nmsReportCommand);
            routerCommand.ShowDialog();
        }

        public void RouterMapOver()
        {
            isRouterMouseOver = true;
        }

        public void RouterMapOut()
        {
            isRouterMouseOver = false;
        }

        public void GetNoMapImage()
        {
            Image image = Properties.Resources.web_init_image;
            string base64Image = CelotUtility.GetBase64FromImage(image);
            base64Image = String.Format(@"data:image/jpg;base64,{0}", base64Image);

            this.webBrowser1.Document.InvokeScript(
                "setNoMapImage",
                new object[] { base64Image });
            image = null;
        }

        public void CallMoveDevice(string routerName, int serialNo, string routerIp)
        {
           // parent.MoveDeviceView(routerName, serialNo, routerIp);
        }

        public void MapInitalizeFinished()
        {
            this.webBrowser1.Document.InvokeScript(
                "layerEnableOnMap",
                new object[] { this.layerEnableOnMap });
            this.webBrowser1.Document.InvokeScript(
                "addCommonMarker",
                new object[] { 
                    "Celot Router Control Center", "수도원 관공서 (group: KSP)  라우터를 관리합니다", 37.3972705, 126.9520715, "110.45.140.93","경기도 안양시 동안구 관양동 1588-10 "
                });

            this.MoveToMapPosition(ApplicationConfig.Instance().Latitude, ApplicationConfig.Instance().Longitude);
            this.PrepareDashboard();
            ApplicationCache.Instance().NMSDataNotify += new NMSDataNotiHandler(this.DashBoard_NMSDataNotify);
        }

        private void DashBoard_NMSDataNotify(object sender, NMSDataNotifyEventArgs args)
        {
            
            this.Invoke(new Action(delegate()
            {
                StringBuilder stringBuilder = new StringBuilder();
                NMSDataWrapper nmsDataWrapper = args.Data;
                this.deviceCountLabel.Text = stringBuilder.AppendFormat("수량 : {0}", ApplicationCache.Instance().Devices.Count).ToString();
                stringBuilder.Clear();
                this.noNMSCountlabel.Text = stringBuilder.AppendFormat("장애 : {0}", nmsDataWrapper.RouterErrCnt).ToString();
                stringBuilder.Clear();
                this.normalDeviceCountLabel.Text = stringBuilder.AppendFormat("정상 : {0}", ApplicationCache.Instance().Devices.Count - nmsDataWrapper.RouterErrCnt).ToString();
                stringBuilder.Clear();

                NMSReportCommand errCommand = nmsDataWrapper.GetRecentAlertNMSReportCommand();
                if (errCommand != null)
                {
                   stringBuilder.AppendFormat(
                        "1.Name : {0}\n2.Version : {1}\n3.Time : {2}\n4.Type : {3}\n5.Error Detai : {4}",
                        errCommand.Device.Name,
                        errCommand.nms_reprot_t == null ? "" : errCommand.nms_reprot_t.data.hw_version,
                        errCommand.nms_reprot_t == null ? "" : CelotUtility.ChangeStampStringToLocalFormat(errCommand.nms_reprot_t.data.current_time),
                        errCommand.AlertList[0].AlertType.Parse(),
                        errCommand.GetAlertMessages().Trim().Substring(0).Replace(",", "\n-"));
                }
                
                //디바이스 정보를 구글맵 디바이스 레이어로 전달
                this.DispatchEventDataToMap(
                       ApplicationCache.Instance().Devices.Count,
                       ApplicationCache.Instance().Devices.Count - nmsDataWrapper.RouterErrCnt,
                       nmsDataWrapper.RouterErrCnt,
                       stringBuilder.ToString().Replace("\n", "<br/>")
               );
                stringBuilder.Clear();

                //맵 마커로 등록할 라우터 필터
                List<NMSReportCommand> mapCommandList = new List<NMSReportCommand>();
                foreach (NMSReportCommand report in nmsDataWrapper.NMSReportCommandList)
                {
                    if (report.isRegisterdDevice())
                    {
                        mapCommandList.Add(report);
                    }
                }
                if (mapCommandList.Count > 0)
                {
                    if (CelotUtility.IsConnectedToInternet())
                    {
                        //marker 와 infowindow 를 재 사용함으로 마우스 팅김 현상에 없어졌으로, 아래 if는 언제나 true로 지정
                        if (true /*!this.isRouterMouseOver*/)
                        {
                            //marker 와 infowindow 를 재 사용함으로 아래의 모든 마커 삭제 구문은 주석
                            //this.webBrowser1.Document.InvokeScript("clearAllMarkerAndInfowindow");
                            this.AddAllDeviceToMap(mapCommandList);
                        }
                    }
                }
                this.SetTechnologyChart(nmsDataWrapper.TechStats);
                this.SetRssiLevelChart(nmsDataWrapper.RssiLevelStats);
                this.setAlertChart(nmsDataWrapper.AllAlertsStats);
                mapCommandList = null;
                nmsDataWrapper = null;
            }));
        }

        public void PrepareDashboard()
        {
            if (ApplicationCache.Instance().CurNMSDataWrapper == null) return;
            NMSDataWrapper nmsDataWrapper = ApplicationCache.Instance().CurNMSDataWrapper;
            this.deviceCountLabel.Text = String.Format("수량 : {0}", ApplicationCache.Instance().Devices.Count);
            this.noNMSCountlabel.Text = String.Format("장애 : {0}", nmsDataWrapper.RouterErrCnt);
            this.normalDeviceCountLabel.Text = String.Format("정상 : {0}", ApplicationCache.Instance().Devices.Count - nmsDataWrapper.RouterErrCnt);

            NMSReportCommand errCommand = nmsDataWrapper.GetRecentAlertNMSReportCommand();
            string statusMessageOnMap = "";
            if (errCommand != null)
            {
                statusMessageOnMap = String.Format(
                    "1.Name : {0}\n2.Version : {1}\n3.Time : {2}\n4.Type : {3}\n5.Error Detai : {4}",
                    errCommand.Device.Name,
                    errCommand.nms_reprot_t == null ? "" : errCommand.nms_reprot_t.data.hw_version,
                    errCommand.nms_reprot_t == null ? "" : CelotUtility.ChangeStampStringToLocalFormat(errCommand.nms_reprot_t.data.current_time),
                    errCommand.AlertList[0].AlertType.Parse(),
                    errCommand.GetAlertMessages().Trim().Substring(0).Replace(",", "\n-")
                );
            }

            //디바이스 정보를 구글맵 디바이스 레이어로 전달
            this.DispatchEventDataToMap(
                   ApplicationCache.Instance().Devices.Count,
                   ApplicationCache.Instance().Devices.Count - nmsDataWrapper.RouterErrCnt,
                   nmsDataWrapper.RouterErrCnt,
                   statusMessageOnMap.Replace("\n", "<br/>")
           );

            List<NMSReportCommand> mapCommandList = new List<NMSReportCommand>();
            foreach(NMSReportCommand report in nmsDataWrapper.NMSReportCommandList){
                if (report.isRegisterdDevice())
                {
                    mapCommandList.Add(report);
                }
            }
            if (mapCommandList.Count > 0)
            {
                if (CelotUtility.IsConnectedToInternet())
                {
                    if (!this.isRouterMouseOver)
                    {
                        this.webBrowser1.Document.InvokeScript("deleteAllInfowindowsAndMarker");
                        this.AddAllDeviceToMap(mapCommandList);
                    }
                }
            }
            this.SetTechnologyChart(nmsDataWrapper.TechStats);
            this.SetRssiLevelChart(nmsDataWrapper.RssiLevelStats);
            this.setAlertChart(nmsDataWrapper.AllAlertsStats);
            nmsDataWrapper = null;
        }

        public void MoveToMapPosition(double  lat, double lng)
        {
            this.webBrowser1.Document.InvokeScript("setMapByCoord", new object[] { lat, lng });
        }

        private void coords_Click(object sender, EventArgs e)
        {
            CoordForm coodForm = new CoordForm();
            if (coodForm.ShowDialog() == DialogResult.OK)
            {
                Double lat = coodForm.Latitude;
                Double lng = coodForm.Longitude;
                this.MoveToMapPosition(lat, lng);
            }
        }

        private void AddAllDeviceToMap(List<NMSReportCommand> nmsReportCommandList)
        {
                foreach (NMSReportCommand nmsReportCommand in nmsReportCommandList)
                {
                    this.AddDeviceToMap(nmsReportCommand);
                }
        }

        private void AddDeviceToMap(NMSReportCommand nmsReportCommand)
        {
            CelotMClient.Model.Device device = nmsReportCommand.Device;
            nms_reprot_t nms = nmsReportCommand.nms_reprot_t;

            string routerName = device.Name;
            int serialNo = device.SerialNo;
            string number = CelotUtility.ChangePhoneNumberToFormatString(device.PhoneNumber);
            string des = device.Des;
            string latitude = device.Latitude;
            string longitude = device.Longitude;
            string routerIp = device == null ? "" : device.RouterIp;
            string lanIp = nms == null ? "": nmsReportCommand.GetLanIPString();
            string wanIp = nms == null ? "" : nmsReportCommand.GetWanIPString();
            string message = nmsReportCommand.GetAlertMessages() !=null ? nmsReportCommand.GetAlertMessages().Substring(1):"";
            string rssi = nms == null ? "" : String.Format("{0} ({1})", nms.data.modulesignal, this.alertManager.getRssiLevel(nmsReportCommand.nms_reprot_t.data.modulesignal ));
            string firmVersion = nms == null ? "" : nms.data.sw_version;
            string nmsVersion = nms == null ? "" : nms.header.pro_ver.ToString();
            
            int routerStatus = 0;
            switch(nmsReportCommand.DeviceAlertCassification){
                case DeviceAlertCassification.NORMAL:
                    routerStatus = 0;
                    break;
                case DeviceAlertCassification.ABNORMAL:
                    routerStatus = 1;
                    break;
                case DeviceAlertCassification.ALERT:
                    routerStatus = 2;
                    break;
            }

            this.webBrowser1.Document.InvokeScript(
                "addMarker", new object[] { 
                    latitude,
                    longitude,
                    routerName, 
                    des,
                    serialNo,
                    number,
                    rssi,
                    routerIp,
                    lanIp,
                    wanIp,
                    firmVersion,
                    nmsVersion,
                    message,
                    routerStatus 
                }
            );
        }

        public void DispatchEventDataToMap(int totalRouterCnt, int normalRouterCnt, int abnormalRouterCnt, string message)
        {
            this.webBrowser1.Document.InvokeScript(
                "setRouterStatusOnMap",
                new object[] { totalRouterCnt, normalRouterCnt, abnormalRouterCnt, message }
                );
        }

        private void SetTechnologyChart(List<NameValuePair> pairList)
        {
            techPieChart.DataSource = null;
            techPieChart.DataSource = pairList;
            techPieChart.Invalidate();
        }

        public void setAlertChart(List<NameValuePair> pairList)
        {
            alertChart.DataSource = null;
            alertChart.DataSource = pairList;
            alertChart.Invalidate();
        }

        private void SetRssiLevelChart(List<NameValuePair> pairList)
        {
            rssiPieChart.DataSource = null;
            rssiPieChart.DataSource = pairList;
            rssiPieChart.Invalidate();
        }

       
        private void chartExtend_Click(object sender, EventArgs e)
        {
            if (isMapExtend)
            {
                this.tableLayoutPanel1.RowStyles[3].Height = this.chartRowHeight;
                this.chartExtend.Text = "▼";
                this.isMapExtend = false;
            }
            else
            {
                this.tableLayoutPanel1.RowStyles[3].Height = 0;
                this.chartExtend.Text = "▲";
                this.isMapExtend = true;
            }
            this.tableLayoutPanel1.PerformLayout();
        }
    }
}
